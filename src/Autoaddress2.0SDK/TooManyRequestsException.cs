using System;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Exception thrown when too many requests submitted.
    /// </summary>
    public class TooManyRequestsException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TooManyRequestsException(string message) : base(message)
        {
        }
    }
}
