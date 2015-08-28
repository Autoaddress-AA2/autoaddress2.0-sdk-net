using System;

namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Container for parameters of PostcodeLookup
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="postcode">Postcode to find.</param>
        /// <param name="language">Language for returned address.</param>
        /// <param name="country">Country the address should be searched in.</param>
        /// <param name="limit">An upper limit on the number of options that may be returned.</param>
        /// <exception cref="ArgumentNullException">postcode</exception>
        public Request(string postcode, Language language, Country country, int limit)
        {
            if (string.IsNullOrWhiteSpace(postcode)) throw new ArgumentNullException("postcode");
            
            Postcode = postcode;
            Language = language;
            Country = country;
            Limit = limit;
        }

        /// <summary>
        /// Gets the postcode.
        /// </summary>
        public string Postcode { get; private set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        public Language Language { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public Country Country { get; private set; }

        /// <summary>
        /// Gets the limit.
        /// </summary>
        public int Limit { get; private set; }
    }
}