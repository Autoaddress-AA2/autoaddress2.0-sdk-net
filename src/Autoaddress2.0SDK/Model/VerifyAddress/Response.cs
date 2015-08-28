using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.VerifyAddress
{
    /// <summary>
    /// VerifyAddress response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(ReturnCode result,
                        string[] postalAddress,
                        string postcode,
                        int? addressId,
                        AddressType? addressType,
                        MatchLevel? matchLevel,
                        Request input,
                        Link[] links)
        {
            Result = result;
            PostalAddress = postalAddress;
            Postcode = postcode;
            AddressId = addressId;
            AddressType = addressType;
            MatchLevel = matchLevel;
            Input = input;
            Links = links;
        }

        /// <summary>
        /// Gets the result of the search.
        /// </summary>
        public ReturnCode Result { get; private set; }
        
        /// <summary>
        /// Gets the postal address in requested language for the input address.
        /// </summary>
        public string[] PostalAddress { get; private set; }
        
        /// <summary>
        /// Gets the Eircode or postcode.
        /// </summary>
        public string Postcode { get; private set; }
        
        /// <summary>
        /// Gets the address id.
        /// </summary>
        public int? AddressId { get; private set; }
        
        /// <summary>
        /// Gets the type of address found.
        /// </summary>
        public AddressType? AddressType { get; private set; }
        
        /// <summary>
        /// Gets the match level of address found.
        /// </summary>
        public MatchLevel? MatchLevel { get; private set; }

        /// <summary>
        /// Gets the input request.
        /// </summary>
        public Request Input { get; private set; }
        
        /// <summary>
        /// Gets an array of Link objects.
        /// </summary>
        public Link[] Links { get; private set; }
    }
}