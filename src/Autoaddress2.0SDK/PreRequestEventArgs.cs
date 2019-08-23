using System;
using System.Net.Http;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Provides data for the PreRequest event.
    /// </summary>
    public class PreRequestEventArgs : EventArgs
    {
        /// <summary>
        /// FindAddress, AutoComplete or other request object.
        /// </summary>
        public object AutoaddressRequest { get; }

        /// <summary>
        /// HTTP message that will be sent to the Autoaddress endpoint.
        /// </summary>
        public HttpRequestMessage HttpRequestMessage { get; }

        /// <summary>
        /// Initializes a new instance of the PreRequestEventArgs class.
        /// </summary>
        /// <param name="autoaddressRequest">FindAddress, AutoComplete or other request object.</param>
        /// <param name="httpRequestMessage">HTTP message that will be sent to the Autoaddress endpoint.</param>
        public PreRequestEventArgs(object autoaddressRequest, HttpRequestMessage httpRequestMessage)
        {
            AutoaddressRequest = autoaddressRequest;
            HttpRequestMessage = httpRequestMessage;
        }
    }
}
