using System;
using System.Net;
using Autoaddress.Autoaddress2_0.Model;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Exception for the Autoaddress 2.0 service.
    /// </summary>
    public class AutoaddressException : Exception
    {
        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        public ErrorType ErrorType { get; }

        /// <summary>
        /// Gets the status code of response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets request Uri.
        /// </summary>
        public Uri RequestUri { get; }

        internal AutoaddressException(ErrorType errorType, HttpStatusCode httpStatusCode, Uri requestUri, string message)
            : base(message)
        {
            ErrorType = errorType;
            HttpStatusCode = httpStatusCode;
            RequestUri = requestUri;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ErrorType=[{ErrorType}], HttpStatusCode=[{HttpStatusCode}], RequestUri=[{RequestUri}], {base.ToString()}";
        }
    }
}