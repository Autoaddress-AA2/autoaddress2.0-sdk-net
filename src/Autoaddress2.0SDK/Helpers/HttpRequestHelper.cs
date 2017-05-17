using System;
using System.Net.Http;
using System.Threading.Tasks;
using Autoaddress.Autoaddress2_0.Model;
using Newtonsoft.Json.Linq;

namespace Autoaddress.Autoaddress2_0.Helpers
{
    internal static class HttpRequestHelper
    {
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
