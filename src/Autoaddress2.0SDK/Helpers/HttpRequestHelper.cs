using Autoaddress.Autoaddress2_0.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autoaddress.Autoaddress2_0.Helpers
{
    internal static class HttpRequestHelper
    {
        public static async Task<string> InvokeGetRequestAsync(HttpClient httpClient, Uri requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            if ((int)response.StatusCode == 429)
            {
                throw new TooManyRequestsException(response.ReasonPhrase);
            }

            AutoaddressException autoaddressException = GetAutoaddressException(response.StatusCode, requestUri, result);

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
                    ErrorType errorType = (ErrorType) ((int) obj["errors"][0]["type"]["code"]);
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
    }
}