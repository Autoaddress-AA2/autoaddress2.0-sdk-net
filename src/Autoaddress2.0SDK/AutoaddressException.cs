using System;
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
        public ErrorType ErrorType { get; private set; }

        internal AutoaddressException(ErrorType errorType, string message)
            : base(message)
        {
            ErrorType = errorType;
        }
    }
}
