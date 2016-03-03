namespace Autoaddress.Autoaddress2_0.Model.ReverseGeocode
{
    /// <summary>
    /// Container for parameters of ReverseGeocode
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="latitude">Latitude (ETRS89).</param>
        /// <param name="longitude">Longitude (ETRS89).</param>
        /// <param name="maxDistance">maximum distance to search from location in metres. Must be less than or equal to 100.</param>
        /// <param name="language">Language.</param>
        /// <param name="country">Country.</param>
        /// <param name="vanityMode">Return vanity address format, if it exists.</param>
        /// <param name="addressProfileName">If supplied, a reformatted address (according to profile rules) is returned in the ReformattedAddress field.</param>
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        public Request(double latitude, double longitude, double maxDistance, Language language, Country country, bool vanityMode, string addressProfileName, string txn = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            MaxDistance = maxDistance;
            Language = language;
            Country = country;
            VanityMode = vanityMode;
            AddressProfileName = addressProfileName;
            Txn = txn;
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// Gets the maximum distance to search from location in metres.
        /// </summary>
        public double MaxDistance { get; private set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        public Language Language { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public Country Country { get; private set; }

        /// <summary>
        /// Gets vanity mode.
        /// </summary>
        public bool VanityMode { get; private set; }

        /// <summary>
        /// Gets the address profile name.
        /// </summary>
        public string AddressProfileName { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }
    }
}