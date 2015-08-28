using System;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Configuration for accessing the Autoaddress 2.0 service.
    /// </summary>
    public class AutoaddressConfig
    {
        /// <summary>
        /// The default API base address (https://api.autoaddress.ie)
        /// </summary>
        public const string DefaultApiBaseAddress = "https://api.autoaddress.ie";
        
        /// <summary>
        /// The default request timeout in milliseconds (2000)
        /// </summary>
        public const int DefaultRequestTimeoutMilliseconds = 2000;

        /// <summary>
        /// Gets the API base address
        /// </summary>
        public string ApiBaseAddress { get; private set; }
        
        /// <summary>
        /// Gets the request timeout in milliseconds
        /// </summary>
        public int RequestTimeoutMilliseconds { get; private set; }

        /// <summary>
        /// Default constructor.
        /// Uses default settings.
        /// </summary>
        public AutoaddressConfig()
        {
            ApiBaseAddress = DefaultApiBaseAddress;
            RequestTimeoutMilliseconds = DefaultRequestTimeoutMilliseconds;
        }

        /// <summary>
        /// Constructs an AutoaddressConfig with an API base address and the request timeout in milliseconds
        /// </summary>
        /// <param name="apiBaseAddress">API base address</param>
        /// <param name="requestTimeoutMilliseconds">Request timeout in milliseconds</param>
        public AutoaddressConfig(string apiBaseAddress = DefaultApiBaseAddress, int requestTimeoutMilliseconds = DefaultRequestTimeoutMilliseconds)
        {
            if (string.IsNullOrEmpty(apiBaseAddress))
            {
                throw new ArgumentNullException("apiBaseAddress");
            }

            if (requestTimeoutMilliseconds <= 0)
            {
                throw new ArgumentOutOfRangeException("requestTimeoutMilliseconds");
            }
            
            ApiBaseAddress = apiBaseAddress;
            RequestTimeoutMilliseconds = requestTimeoutMilliseconds;
        }
    }
}
