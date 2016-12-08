using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Autoaddress.Autoaddress2_0.Model;
using Newtonsoft.Json.Linq;

namespace Autoaddress.Autoaddress2_0.Helpers
{
    internal static class HttpRequestHelper
    {
        public static string InvokeGetRequest(Uri requestUri, string contentType, int requestTimeoutMilliseconds)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Timeout = requestTimeoutMilliseconds;

            try
            {
                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (var responseStream = httpWebResponse.GetResponseStream())
                    {
                        if (responseStream == null) return null;

                        using (var streamReader = new StreamReader(responseStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError)
                {
                    if (webException.Response != null)
                    {
                        Stream responseStream = webException.Response.GetResponseStream();

                        if (responseStream != null)
                        {
                            var result = new StreamReader(responseStream).ReadToEnd();
                            AutoaddressException autoaddressException = GetAutoaddressException(result);

                            if (autoaddressException != null)
                            {
                                throw autoaddressException;
                            }
                        }
                    }
                }

                throw;
            }
        }

        public static async Task<string> InvokeGetRequestAsync(HttpClient httpClient, Uri requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            AutoaddressException autoaddressException = GetAutoaddressException(result);

            if (autoaddressException != null)
            {
                throw autoaddressException;
            }

            response.EnsureSuccessStatusCode();

            return result;
        }

        private static AutoaddressException GetAutoaddressException(string response)
        {
            JObject obj = JObject.Parse(response);
            AutoaddressException autoaddressException = null;

            if (obj["errors"] != null && obj["errors"].HasValues && obj["errors"][0]["type"]["code"] != null && obj["errors"][0]["message"] != null)
            {
                ErrorType errorType = (ErrorType)((int)obj["errors"][0]["type"]["code"]);
                string message = obj["errors"][0]["message"].ToString();
                autoaddressException = new AutoaddressException(errorType, message);
            }

            return autoaddressException;
        }
    }
}
