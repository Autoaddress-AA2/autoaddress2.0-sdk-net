using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// PostcodeLookup response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(ReturnCode result,
            string postcode,
            int? addressId,
            AddressType? addressType, 
            MatchLevel matchLevel,
            string[] postalAddress,
            int totalOptions,
            Option[] options,
            Request input,
            Link[] links
            )
        {
            Result = result;
            Postcode = postcode;
            AddressId = addressId;
            AddressType = addressType;
            MatchLevel = matchLevel;
            PostalAddress = postalAddress;
            TotalOptions = totalOptions;
            Options = options;
            Input = input;
            Links = links;
        }

        /// <summary>
        /// Gets the result of the lookup.
        /// </summary>
        public ReturnCode Result { get; private set; }

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
        public MatchLevel MatchLevel { get; private set; }
        
        /// <summary>
        /// Gets the postal address in requested language for the input address.
        /// </summary>
        public string[] PostalAddress { get; private set; }
        
        /// <summary>
        /// Gets the total number of options. If this value is greater than the value of input field limit then no option is returned.
        /// </summary>
        public int TotalOptions { get; private set; }
        
        /// <summary>
        /// Gets an array of Option objects.
        /// </summary>
        public Option[] Options { get; private set; }

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