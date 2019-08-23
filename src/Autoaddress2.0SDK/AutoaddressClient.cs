using Autoaddress.Autoaddress2_0.Extensions;
using Autoaddress.Autoaddress2_0.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Client for accessing the Autoaddress 2.0 service.
    /// </summary>
    /// <remarks>
    /// The AutoaddressClient provides methods for calling the various Autoaddress 2.0 service methods.
    /// </remarks>
    /// <example>
    /// The following code example creates an AutoaddressClient and makes a FindAddress call.
    /// The licence key is retrieved from the application's default configuration.
    /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientExample1.cs" language="cs" />
    /// The following code example creates an AutoaddressClient using a licence key and makes a FindAddress call.
    /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientExample2.cs" language="cs" />
    /// The following code example creates an AutoaddressClient using a licence key and Autoaddress config then makes a FindAddress call.
    /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientExample3.cs" language="cs" />
    /// </example>
    public class AutoaddressClient : IAutoaddress
    {
        private const string Version = "2.0";
        private const string FindAddressMethod = "FindAddress";
        private const string PostcodeLookupMethod = "PostcodeLookup";
        private const string VerifyAddressMethod = "VerifyAddress";
        private const string GetEcadDataMethod = "GetEcadData";
        private const string AutoCompleteMethod = "AutoComplete";
        private const string ReverseGeocodeMethod = "ReverseGeocode";
        private const string GetGbBuildingDataMethod = "GetGbBuildingData";
        private const string GetGbPostcodeDataMethod = "GetGbPostcodeData";
        private const string MapIdMethod = "MapId";
        private const string JsonContentType = "application/json";

        private static readonly string AssemblyVersion;

        private readonly AutoaddressConfig _autoaddressConfig;
        private readonly string _licenceKey;
        private readonly object _httpClientLock = new object();
        private HttpClient _httpClient;

        /// <summary>
        /// Occurs just before request is sent to Autoaddress endpoint.
        /// </summary>
        public event EventHandler<PreRequestEventArgs> PreRequest;

        static AutoaddressClient()
        {
            string assemblyQualifiedName = typeof(AutoaddressClient).AssemblyQualifiedName;
            if (assemblyQualifiedName != null)
            {
                string version = assemblyQualifiedName
                    .Split(',')
                    .Where(x => !string.IsNullOrWhiteSpace(x) && x.ToLowerInvariant().Contains("version"))
                    .Select(x => x.Trim())
                    .FirstOrDefault();
                AssemblyVersion = version;
            }
        }

        /// <summary>
        /// Constructs AutoaddressClient with a licence key and the other configuration settings using defaults.
        /// </summary>
        /// <param name="licenceKey">The licence key.</param>
        /// <exception cref="ArgumentNullException">licenceKey</exception>
        public AutoaddressClient(string licenceKey)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            
            _licenceKey = licenceKey;
            _autoaddressConfig = new AutoaddressConfig();
        }

        /// <summary>
        /// Constructs AutoaddressClient with a licence key and an AutoaddressConfig object.
        /// </summary>
        /// <param name="licenceKey">The licence key.</param>
        /// <param name="autoaddressConfig">The autoaddress config.</param>
        /// <exception cref="System.ArgumentNullException">
        /// licenceKey
        /// or
        /// autoaddressConfig
        /// </exception>
        public AutoaddressClient(string licenceKey, AutoaddressConfig autoaddressConfig)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));

            _licenceKey = licenceKey;
            _autoaddressConfig = autoaddressConfig ?? throw new ArgumentNullException(nameof(autoaddressConfig));
        }

        private static async Task<string> InvokeGetRequestAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            var completionOption = HttpCompletionOption.ResponseContentRead;
            var cancellationToken = CancellationToken.None;

            HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage, completionOption, cancellationToken).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            if ((int)response.StatusCode == 429)
            {
                throw new TooManyRequestsException(response.ReasonPhrase);
            }

            AutoaddressException autoaddressException = GetAutoaddressException(response.StatusCode, httpRequestMessage.RequestUri, result);

            if (autoaddressException != null)
            {
                throw autoaddressException;
            }

            // guard
            response.EnsureSuccessStatusCode();

            throw new InvalidOperationException();
        }

        private static AutoaddressException GetAutoaddressException(HttpStatusCode httpStatusCode, Uri requestUri, string response)
        {
            JObject obj;
            try
            {
                obj = JObject.Parse(response);
            }
            catch
            {
                return new AutoaddressException(ErrorType.Unknown, httpStatusCode, requestUri, $"response=[{response}]");
            }

            try
            {
                AutoaddressException autoaddressException = null;
                if (obj["errors"] != null && obj["errors"].HasValues && obj["errors"][0]["type"]["code"] != null && obj["errors"][0]["message"] != null)
                {
                    ErrorType errorType = (ErrorType)((int)obj["errors"][0]["type"]["code"]);
                    string message = obj["errors"][0]["message"].ToString();
                    autoaddressException = new AutoaddressException(errorType, httpStatusCode, requestUri, message);
                }
                return autoaddressException;
            }
            catch
            {
                return new AutoaddressException(ErrorType.Unknown, httpStatusCode, requestUri, $"response=[{response}]");
            }
        }

        private async Task<T> GetResponseAsync<T>(object request, Uri requestUri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentType));
            httpRequestMessage.Headers.Add("Client", $"Autoaddress2.0SDK {AssemblyVersion}");

            PreRequest?.Invoke(this, new PreRequestEventArgs(request, httpRequestMessage));

            EnsureHttpClient();
            string response = await InvokeGetRequestAsync(_httpClient, httpRequestMessage).ConfigureAwait(false);

            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address. Returns all available data if found.
        /// </summary>
        /// <param name="request">FindAddress request.</param>
        /// <returns>FindAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddress with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressRequestExample1.cs" language="cs" />
        /// </example>
        public Model.FindAddress.Response FindAddress(Model.FindAddress.Request request)
        {
            try
            {
                return FindAddressAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Lookup a Postcode or Address. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a FindAddress response.</param>
        /// <returns>FindAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddress then calls FindAddress again with an option link.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressLinkExample1.cs" language="cs" />
        /// </example>
        public Model.FindAddress.Response FindAddress(Model.FindAddress.Link link)
        {
            try
            {
                return FindAddressAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">FindAddress request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddressAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, FindAddressMethod, request);
            var response = await GetResponseAsync<Model.FindAddress.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a FindAddress response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddressAsync then calls FindAddressAsync again with an option link.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressAsyncLinkExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.FindAddress.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Lookup a Postcode. Returns all available data if found.
        /// </summary>
        /// <param name="request">PostcodeLookup request.</param>
        /// <returns>PostcodeLookup response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookup with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupRequestExample1.cs" language="cs" />
        /// </example>
        public Model.PostcodeLookup.Response PostcodeLookup(Model.PostcodeLookup.Request request)
        {
            try
            {
                return PostcodeLookupAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Lookup a Postcode. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a PostcodeLookup response.</param>
        /// <returns>PostcodeLookup response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookup with a request then calls PostcodeLookup again with one of the option links.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupLinkExample1.cs" language="cs" />
        /// </example>
        public Model.PostcodeLookup.Response PostcodeLookup(Model.PostcodeLookup.Link link)
        {
            try
            {
                return PostcodeLookupAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">PostcodeLookup request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookupAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, PostcodeLookupMethod, request);
            var response = await GetResponseAsync<Model.PostcodeLookup.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a PostcodeLookup response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookupAsync with a request then calls PostcodeLookupAsync again with one of the option links.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupAsyncLinkExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.PostcodeLookup.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied.
        /// </summary>
        /// <param name="request">VerifyAddress request.</param>
        /// <returns>VerifyAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls VerifyAddress with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientVerifyAddressRequestExample1.cs" language="cs" />
        /// </example>
        public Model.VerifyAddress.Response VerifyAddress(Model.VerifyAddress.Request request)
        {
            try
            {
                return VerifyAddressAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>VerifyAddress response.</returns>
        public Model.VerifyAddress.Response VerifyAddress(Model.VerifyAddress.Link link)
        {
            try
            {
                return VerifyAddressAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="request">VerifyAddress request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls VerifyAddressAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientVerifyAddressAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, VerifyAddressMethod, request);
            var response = await GetResponseAsync<Model.VerifyAddress.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));
            
            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.VerifyAddress.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id.
        /// </summary>
        /// <param name="request">GetEcadData request.</param>
        /// <returns>GetEcadData response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetEcadData with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetEcadDataRequestExample1.cs" language="cs" />
        /// </example>
        public Model.GetEcadData.Response GetEcadData(Model.GetEcadData.Request request)
        {
            try
            {
                return GetEcadDataAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>GetEcadData response.</returns>
        public Model.GetEcadData.Response GetEcadData(Model.GetEcadData.Link link)
        {
            try
            {
                return GetEcadDataAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetEcadData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetEcadDataAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetEcadDataAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, GetEcadDataMethod, request);
            var response = await GetResponseAsync<Model.GetEcadData.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.GetEcadData.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Lookup a Postcode or Address auto complete options. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>AutoComplete response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls AutoComplete with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientAutoCompleteRequestExample1.cs" language="cs" />
        /// </example>
        public Model.AutoComplete.Response AutoComplete(Model.AutoComplete.Request request)
        {
            try
            {
                return AutoCompleteAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Lookup a Postcode or Address auto complete options as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls AutoCompleteAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientAutoCompleteAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.AutoComplete.Response> AutoCompleteAsync(Model.AutoComplete.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, AutoCompleteMethod, request);
            var response = await GetResponseAsync<Model.AutoComplete.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Reverse geocode a location. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="request">ReverseGeocode request.</param>
        /// <returns>ReverseGeocode response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls ReverseGeocode with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientReverseGeocodeRequestExample1.cs" language="cs" />
        /// </example>
        public Model.ReverseGeocode.Response ReverseGeocode(Model.ReverseGeocode.Request request)
        {
            try
            {
                return ReverseGeocodeAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Reverse geocode a location. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="link">A link returned in a ReverseGeocode response.</param>
        /// <returns>ReverseGeocode response.</returns>
        public Model.ReverseGeocode.Response ReverseGeocode(Model.ReverseGeocode.Link link)
        {
            try
            {
                return ReverseGeocodeAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Reverse geocode a location as an asynchronous operation. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="request">ReverseGeocode request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls ReverseGeocodeAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientReverseGeocodeAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.ReverseGeocode.Response> ReverseGeocodeAsync(Model.ReverseGeocode.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, ReverseGeocodeMethod, request);
            var response = await GetResponseAsync<Model.ReverseGeocode.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Reverse geocode a location as an asynchronous operation. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="link">A link returned in a ReverseGeocode response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.ReverseGeocode.Response> ReverseGeocodeAsync(Model.ReverseGeocode.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.ReverseGeocode.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Return data for the supplied UK address ID.
        /// </summary>
        /// <param name="request">GetGbBuildingData request.</param>
        /// <returns>GetGbBuildingData response.</returns>
        public Model.GetGbBuildingData.Response GetGbBuildingData(Model.GetGbBuildingData.Request request)
        {
            try
            {
                return GetGbBuildingDataAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return data for the supplied UK address ID.
        /// </summary>
        /// <param name="link">A link returned in a GetGbBuildingData response.</param>
        /// <returns>GetGbBuildingData response.</returns>
        public Model.GetGbBuildingData.Response GetGbBuildingData(Model.GetGbBuildingData.Link link)
        {
            try
            {
                return GetGbBuildingDataAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return data for the supplied UK address ID as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetGbBuildingData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.GetGbBuildingData.Response> GetGbBuildingDataAsync(Model.GetGbBuildingData.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, GetGbBuildingDataMethod, request);
            var response = await GetResponseAsync<Model.GetGbBuildingData.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Return data for the supplied UK address ID as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetGbBuildingData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.GetGbBuildingData.Response> GetGbBuildingDataAsync(Model.GetGbBuildingData.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.GetGbBuildingData.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Return data for the supplied UK postcode.
        /// </summary>
        /// <param name="request">GetGbPostcodeData request.</param>
        /// <returns>GetGbPostcodeData response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetGbPostcodeData with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetGbPostcodeDataRequestExample1.cs" language="cs" />
        /// </example>
        public Model.GetGbPostcodeData.Response GetGbPostcodeData(Model.GetGbPostcodeData.Request request)
        {
            try
            {
                return GetGbPostcodeDataAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return data for the supplied UK postcode.
        /// </summary>
        /// <param name="link">A link returned in a GetGbPostcodeData response.</param>
        /// <returns>GetGbPostcodeData response.</returns>
        public Model.GetGbPostcodeData.Response GetGbPostcodeData(Model.GetGbPostcodeData.Link link)
        {
            try
            {
                return GetGbPostcodeDataAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Return data for the supplied UK postcode as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetGbPostcodeData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetGbPostcodeDataAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetGbPostcodeDataAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.GetGbPostcodeData.Response> GetGbPostcodeDataAsync(Model.GetGbPostcodeData.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, GetGbPostcodeDataMethod, request);
            var response = await GetResponseAsync<Model.GetGbPostcodeData.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Return data for the supplied UK postcode as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetGbPostcodeData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.GetGbPostcodeData.Response> GetGbPostcodeDataAsync(Model.GetGbPostcodeData.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.GetGbPostcodeData.Response>(link, requestUri);
            return response;
        }

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="request">MapId request.</param>
        /// <returns>MapId response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls MapId with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetGbPostcodeDataRequestExample1.cs" language="cs" />
        /// </example>
        public Model.MapId.Response MapId(Model.MapId.Request request)
        {
            try
            {
                return MapIdAsync(request).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="link">A link returned in a MapId response.</param>
        /// <returns>MapId response.</returns>
        public Model.MapId.Response MapId(Model.MapId.Link link)
        {
            try
            {
                return MapIdAsync(link).Result;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        /// <summary>
        /// Map ID as an asynchronous operation.
        /// </summary>
        /// <param name="request">MapId request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetGbPostcodeDataAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetGbPostcodeDataAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.MapId.Response> MapIdAsync(Model.MapId.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Uri requestUri = GetRequestUri(_licenceKey, request.Txn, _autoaddressConfig.ApiBaseAddress, Version, MapIdMethod, request);
            var response = await GetResponseAsync<Model.MapId.Response>(request, requestUri);
            return response;
        }

        /// <summary>
        /// Map ID as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a MapId response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Model.MapId.Response> MapIdAsync(Model.MapId.Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            Uri requestUri = link.Href;
            var response = await GetResponseAsync<Model.MapId.Response>(link, requestUri);
            return response;
        }

        private static Uri GetRequestUri(string licenceKey, string txn, string baseAddress, string version, string method, object inputParam)
        {
            string requestUri = $"{baseAddress}/{version}/{method}";

            // note: it must be "key", not "Key" or any other caseing
            var parameters = !string.IsNullOrEmpty(txn) ? 
                                    $"key={licenceKey}&txn={txn}&{inputParam.ToQueryString()}" : 
                                    $"key={licenceKey}&{inputParam.ToQueryString()}";

            requestUri = $"{requestUri}?{parameters}";
            return new Uri(requestUri);
        }

        private static string ParseJson(string json)
        {
            JObject code = JObject.Parse(json);

            if (code["ecadIdStatus"]?["code"] != null)
                code["ecadIdStatus"] = (string)code["ecadIdStatus"]["code"];

            if (code["matchLevel"]?["code"] != null)
                code["matchLevel"] = (string) code["matchLevel"]["code"];

            if (code["addressType"]?["code"] != null)
                code["addressType"] = (string) code["addressType"]["code"];

            if (code["reformattedAddressResult"]?["code"] != null)
                code["reformattedAddressResult"] = (string)code["reformattedAddressResult"]["code"];

            if (code["hits"] != null && code["hits"].Children().Any())
            {
                foreach (JToken hit in code["hits"])
                {
                    if (hit["reformattedAddressResult"]?["code"] != null)
                    {
                        hit["reformattedAddressResult"] = (string)hit["reformattedAddressResult"]["code"];
                    }
                }
            }

            if (code["unmatchedAddressElements"] != null && code["unmatchedAddressElements"].Children().Any())
            {
                foreach (JToken unmatchedAddressElement in code["unmatchedAddressElements"])
                {
                    if (unmatchedAddressElement["type"]?["code"] != null)
                    {
                        unmatchedAddressElement["type"] = (string)unmatchedAddressElement["type"]["code"];
                    }
                }
            }

            if (code["postalAddressElements"] != null && code["postalAddressElements"].Children().Any())
            {
                foreach (JToken postalAddressElement in code["postalAddressElements"])
                {
                    if (postalAddressElement["type"]?["code"] != null)
                    {
                        postalAddressElement["type"] = (string)postalAddressElement["type"]["code"];
                    }
                }
            }

            if (code["geographicAddressElements"] != null && code["geographicAddressElements"].Children().Any())
            {
                foreach (JToken geographicAddressElement in code["geographicAddressElements"])
                {
                    if (geographicAddressElement["type"]?["code"] != null)
                    {
                        geographicAddressElement["type"] = (string)geographicAddressElement["type"]["code"];
                    }
                }
            }

            if (code["vanityAddressElements"] != null && code["vanityAddressElements"].Children().Any())
            {
                foreach (JToken vanityAddressElement in code["vanityAddressElements"])
                {
                    if (vanityAddressElement["type"]?["code"] != null)
                    {
                        vanityAddressElement["type"] = (string)vanityAddressElement["type"]["code"];
                    }
                }
            }

            if (code["options"] != null && code["options"].Children().Any())
            {
                foreach (JToken option in code["options"])
                {
                    if (option["addressType"]?["code"] != null)
                    {
                        option["addressType"] = (string) option["addressType"]["code"];
                    }

                    if (option["optionType"]?["code"] != null)
                    {
                        option["optionType"] = (string) option["optionType"]["code"];
                    }
                }
            }

            if (code["result"]?["code"] != null)
            {
                code["result"] = (string)code["result"]["code"];
            }

            if (code["postcodeNotAvailable"]?["code"] != null)
            {
                code["postcodeNotAvailable"] = (string)code["postcodeNotAvailable"]["code"];
            }

            return code.ToString();
        }

        private void EnsureHttpClient()
        {
            if (_httpClient == null)
            {
                lock (_httpClientLock)
                {
                    if (_httpClient == null)
                    {
                        _httpClient = new HttpClient
                                      {
                                          Timeout = TimeSpan.FromMilliseconds(_autoaddressConfig.RequestTimeoutMilliseconds)
                                      };
                    }
                }
            }
        }
    }
}