using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Option returned in a FindAddress Response object
    /// </summary>
    public class Option
    {
        [JsonConstructor]
        internal Option(string displayName, int? addressId, AddressType? addressType, string postcode, OptionType optionType, Link[] links)
        {
            if (string.IsNullOrWhiteSpace(displayName)) 
                throw new ArgumentNullException("displayName");

            DisplayName = displayName;
            AddressId = addressId;
            AddressType = addressType;
            Postcode = postcode;
            OptionType = optionType;
            Links = links;
        }

        /// <summary>
        /// Gets the type of the option.
        /// </summary>
        public OptionType OptionType { get; private set; }
        
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }
        
        /// <summary>
        /// Gets the postcode.
        /// </summary>
        public string Postcode { get; private set; }
        
        /// <summary>
        /// Gets the address id.
        /// </summary>
        public int? AddressId { get; private set; }

        /// <summary>
        /// Gets the address type.
        /// </summary>
        public AddressType? AddressType { get; private set; }

        /// <summary>
        /// Gets the links.
        /// </summary>
        public Link[] Links { get; private set; }
    }
}