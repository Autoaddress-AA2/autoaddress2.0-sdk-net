using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Autoaddress.Autoaddress2_0.Extensions
{
    internal static class UriExtensions
    {
        /// <summary>
        /// Get query string for specific request
        /// </summary>
        /// <param name="request">Request for Query String</param>
        /// <param name="separator">Seperator used in Query String.</param>
        /// <returns></returns>
        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            Dictionary<string, string> properties = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties.Where(x => x.Value != null)
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }

        public static Uri RemovePort(this Uri uri)
        {
            if (uri == null)
                return null;

            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Port = -1;

            return uriBuilder.Uri;
        }
    }
}
