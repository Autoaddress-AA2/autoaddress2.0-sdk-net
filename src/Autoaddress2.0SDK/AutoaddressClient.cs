using System;
using System.Linq;
using System.Threading.Tasks;
using Autoaddress.Autoaddress2_0.Extensions;
using Autoaddress.Autoaddress2_0.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Client for accessing the Autoaddress 2 service.
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
        private const string JsonContentType = "application/json";

        private readonly AutoaddressConfig _autoaddressConfig;
        private readonly string _licenceKey;

        /// <summary>
        /// Constructs AutoaddressClient with the licence key loaded from the application's
        /// default configuration and the other configuration settings using defaults.
        ///
        /// Example App.config with licence key set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AutoAddress.AutoAddress2_0.Settings.Licence.Key" value="TheLicenceKey"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </summary>
        public AutoaddressClient()
        {
            _licenceKey = Settings.Licence.Key;
            _autoaddressConfig = new AutoaddressConfig();
        }

        /// <summary>
        /// Constructs AutoaddressClient with a licence key and the other configuration settings using defaults.
        /// </summary>
        /// <param name="licenceKey">The licence key.</param>
        /// <exception cref="ArgumentNullException">licenceKey</exception>
        public AutoaddressClient(string licenceKey)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException("licenceKey");
            
            _licenceKey = licenceKey;
            _autoaddressConfig = new AutoaddressConfig();
        }

        /// <summary>
        /// Constructs AutoaddressClient with the licence key loaded from the application's
        /// default configuration and the other configuration settings from an AutoaddressConfig object.
        ///
        /// Example App.config with licence key set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AutoAddress.AutoAddress2_0.Settings.Licence.Key" value="TheLicenceKey"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </summary>
        /// <param name="autoaddressConfig">The autoaddress config.</param>
        /// <exception cref="System.ArgumentNullException">autoaddressConfig</exception>
        public AutoaddressClient(AutoaddressConfig autoaddressConfig)
        {
            if (autoaddressConfig == null) throw new ArgumentNullException("autoaddressConfig");

            _licenceKey = Settings.Licence.Key;
            _autoaddressConfig = autoaddressConfig;
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
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException("licenceKey");
            if (autoaddressConfig == null) throw new ArgumentNullException("autoaddressConfig");

            _licenceKey = licenceKey;
            _autoaddressConfig = autoaddressConfig;
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
            if (request == null) throw new ArgumentNullException("request");
            
            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, FindAddressMethod, request);
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
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
            if (link == null) throw new ArgumentNullException("link");
            
            Uri requestUri = link.Href;
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a AutoComplete response.</param>
        /// <returns>FindAddress response.</returns>
        public Model.FindAddress.Response FindAddress(Model.AutoComplete.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">FindAddress request.</param>
        /// <returns>FindAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddressAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, FindAddressMethod, request);
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a FindAddress response.</param>
        /// <returns>FindAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls FindAddressAsync then calls FindAddressAsync again with an option link.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientFindAddressAsyncLinkExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a AutoComplete response.</param>
        /// <returns>FindAddress response.</returns>
        public async Task<Model.FindAddress.Response> FindAddressAsync(Model.AutoComplete.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.FindAddress.Response>(result);
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
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, PostcodeLookupMethod, request);
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.PostcodeLookup.Response>(result);
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
            if (link == null) throw new ArgumentNullException("link");
            
            Uri requestUri = link.Href;
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.PostcodeLookup.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">PostcodeLookup request.</param>
        /// <returns>PostcodeLookup response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookupAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, PostcodeLookupMethod, request);
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.PostcodeLookup.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a PostcodeLookup response.</param>
        /// <returns>PostcodeLookup response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls PostcodeLookupAsync with a request then calls PostcodeLookupAsync again with one of the option links.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientPostcodeLookupAsyncLinkExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.PostcodeLookup.Response>(result);
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
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, VerifyAddressMethod, request);
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.VerifyAddress.Response>(result);
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>VerifyAddress response.</returns>
        public Model.VerifyAddress.Response VerifyAddress(Model.VerifyAddress.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");
            
            Uri requestUri = link.Href;
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.VerifyAddress.Response>(result);
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="request">VerifyAddress request.</param>
        /// <returns>VerifyAddress response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls VerifyAddressAsync with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientVerifyAddressAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, VerifyAddressMethod, request);
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.VerifyAddress.Response>(result);
        }

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>VerifyAddress response.</returns>
        public async Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");
            
            Uri requestUri = link.Href;
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.VerifyAddress.Response>(result);
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
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, GetEcadDataMethod, request);
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.GetEcadData.Response>(result);
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>GetEcadData response.</returns>
        public Model.GetEcadData.Response GetEcadData(Model.GetEcadData.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.GetEcadData.Response>(result);
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetEcadData request.</param>
        /// <returns>GetEcadData response.</returns>
        /// <example>
        /// The following code example creates an AutoaddressClient and calls GetEcadData with a request.
        /// <code source="..\src\Autoaddress2.0SDK.Test\Example\AutoaddressClientGetEcadDataAsyncRequestExample1.cs" language="cs" />
        /// </example>
        public async Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, GetEcadDataMethod, request);
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.GetEcadData.Response>(result);
        }

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>GetEcadData response.</returns>
        public async Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Link link)
        {
            if (link == null) throw new ArgumentNullException("link");

            Uri requestUri = link.Href;
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.GetEcadData.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address auto complete options. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>AutoComplete response.</returns>
        public Model.AutoComplete.Response AutoComplete(Model.AutoComplete.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, AutoCompleteMethod, request);
            string response = HttpRequestHelper.InvokeGetRequest(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.AutoComplete.Response>(result);
        }

        /// <summary>
        /// Lookup a Postcode or Address auto complete options as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>AutoComplete response.</returns>
        public async Task<Model.AutoComplete.Response> AutoCompleteAsync(Model.AutoComplete.Request request)
        {
            if (request == null) throw new ArgumentNullException("request");

            Uri requestUri = GetRequestUri(_licenceKey, _autoaddressConfig.ApiBaseAddress, Version, AutoCompleteMethod, request);
            string response = await HttpRequestHelper.InvokeGetRequestAsync(requestUri, JsonContentType, _autoaddressConfig.RequestTimeoutMilliseconds);
            string result = ParseJson(response);
            return JsonConvert.DeserializeObject<Model.AutoComplete.Response>(result);
        }

        private static Uri GetRequestUri(string licenceKey, string baseAddress, string version, string method, object inputParam)
        {
            string requestUri = String.Format("{0}/{1}/{2}", baseAddress, version, method);
            string parameters = string.Format("Key={0}&{1}", licenceKey, inputParam.ToQueryString());
            requestUri = String.Format("{0}{1}{2}", requestUri, "?", parameters);
            return new Uri(requestUri);
        }

        private static string ParseJson(string json)
        {
            JObject code = JObject.Parse(json);

            if (code["matchLevel"] != null && code["matchLevel"]["code"] != null)
                code["matchLevel"] = (string) code["matchLevel"]["code"];

            if (code["addressType"] != null && code["addressType"]["code"] != null)
                code["addressType"] = (string) code["addressType"]["code"];

            if (code["options"] != null && code["options"].Children().Any())
            {
                foreach (JToken option in code["options"])
                {
                    if (option["addressType"] != null && option["addressType"]["code"] != null)
                    {
                        option["addressType"] = (string) option["addressType"]["code"];
                    }

                    if (option["optionType"] != null && option["optionType"]["code"] != null)
                    {
                        option["optionType"] = (string) option["optionType"]["code"];
                    }
                }
            }

            if (code["result"] != null && code["result"]["code"] != null)
            {
                code["result"] = (string) code["result"]["code"];
            }

            return code.ToString();
        }
    }
}