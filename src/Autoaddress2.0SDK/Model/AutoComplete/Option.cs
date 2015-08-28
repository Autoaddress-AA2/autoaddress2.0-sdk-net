using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.AutoComplete
{
    /// <summary>
    /// Option returned in a AutoComplete Response object
    /// </summary>
    public class Option
    {
        [JsonConstructor]
        internal Option(string displayName, int addressId, AddressType? addressType, Link[] links)
        {
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException("displayName");
            if (links == null) throw new ArgumentNullException("links");

            DisplayName = displayName;
            AddressId = addressId;
            AddressType = addressType;
            Links = links;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the address id.
        /// </summary>
        public int AddressId { get; set; }
        
        /// <summary>
        /// Gets the address type.
        /// </summary>
        public AddressType? AddressType { get; set; }
        
        /// <summary>
        /// Gets the links.
        /// </summary>
        public Link[] Links { get; set; }
    }
}
