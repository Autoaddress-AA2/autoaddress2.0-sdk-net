using System;

namespace Autoaddress.Autoaddress2_0
{
    public class TooManyRequestsException : Exception
    {
        public TooManyRequestsException(string message) : base(message)
        {
        }
    }
}
