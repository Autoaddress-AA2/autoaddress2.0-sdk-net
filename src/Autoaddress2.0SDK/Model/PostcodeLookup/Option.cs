using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Option returned in a PostcodeLookup Response object
    /// </summary>
    public class Option
    {
        [JsonConstructor]
        internal Option(string displayName, int addressId, AddressType? addressType, Model.Link[] links)
        {
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException("displayName");
            if (links == null) throw new ArgumentNullException("links");

            DisplayName = displayName;
            AddressId = addressId;
            AddressType = addressType;
            
            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "next":
                        newLink = new Model.PostcodeLookup.Link(link.Rel, link.Href);
                        break;
                    default:
                        newLink = link;
                        break;
                }

                newLinks.Add(newLink);
            }

            Links = newLinks.ToArray();
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
        public Model.Link[] Links { get; private set; }
    }
}