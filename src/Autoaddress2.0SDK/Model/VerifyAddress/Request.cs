using System;

namespace Autoaddress.Autoaddress2_0.Model.VerifyAddress
{
    /// <summary>
    /// Container for parameters of VerifyAddress
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="postcode">Postcode to find.</param>
        /// <param name="address">An address separated by commas to compare.</param>
        /// <param name="language">Language to verify address.</param>
        /// <param name="country">Country to verify address.</param>
        /// <exception cref="ArgumentNullException">
        /// postcode
        /// or
        /// address
        /// </exception>
        public Request(string postcode, string address, Language language, Country country)
        {
            if (string.IsNullOrWhiteSpace(postcode)) throw new ArgumentNullException("postcode");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException("address");
            
            Postcode = postcode;
            Address = address;
            Language = language;
            Country = country;
        }

        /// <summary>
        /// Gets the postcode.
        /// </summary>
        public string Postcode { get; private set; }
        
        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address { get; private set; }
        
        /// <summary>
        /// Gets the language.
        /// </summary>
        public Language Language { get; private set; }
        
        /// <summary>
        /// Gets the country.
        /// </summary>
        public Country Country { get; private set; }
    }
}