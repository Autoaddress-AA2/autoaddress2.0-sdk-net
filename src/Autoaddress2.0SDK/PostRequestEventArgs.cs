using System;
using System.Net.Http;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Provides data for the PostRequest event.
    /// </summary>
    public class PostRequestEventArgs : EventArgs
    {
        /// <summary>
        /// HTTP response returned by the Autoaddress endpoint.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Content from the response.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Initializes a new instance of the PostRequestEventArgs class.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response returned by the Autoaddress endpoint</param>
        /// <param name="content">Content from the response.</param>
        public PostRequestEventArgs(HttpResponseMessage httpResponseMessage, string content)
        {
            HttpResponseMessage = httpResponseMessage;
            Content = content;
        }
    }
}
