using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Option returned in a FindAddress Response object
    /// </summary>
    public class Option
    {
        [JsonConstructor]
        internal Option(string displayName, int? addressId, AddressType? addressType, string postcode, OptionType optionType, Model.Link[] links)
        {
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException("displayName");
            if (links == null) throw new ArgumentNullException("links");

            DisplayName = displayName;
            AddressId = addressId;
            AddressType = addressType;
            Postcode = postcode;
            OptionType = optionType;

            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "next":
                        newLink = new Link(link.Rel, link.Href);
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
        public Model.Link[] Links { get; private set; }
    }
}