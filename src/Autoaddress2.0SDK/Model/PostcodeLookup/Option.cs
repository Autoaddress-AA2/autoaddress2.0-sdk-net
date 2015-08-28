using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Option returned in a PostcodeLookup Response object
    /// </summary>
    public class Option
    {
        [JsonConstructor]
        internal Option(string displayName, int addressId, AddressType? addressType, Link[] links)
        {
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException("displayName");
            
            DisplayName = displayName;
            AddressId = addressId;
            AddressType = addressType;
            Links = links;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the address id.
        /// </summary>
        public int AddressId { get; private set; }
        
        /// <summary>
        /// Gets the address type.
        /// </summary>
        public AddressType? AddressType { get; private set; }
        
        /// <summary>
        /// An array of Link objects.
        /// </summary>
        public Link[] Links { get; private set; }
    }
}