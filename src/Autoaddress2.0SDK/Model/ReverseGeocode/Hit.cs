using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.ReverseGeocode
{
    /// <summary>
    /// Hit returned in a ReverseGeocode Response object
    /// </summary>
    public class Hit
    {
        [JsonConstructor]
        internal Hit(int addressId, string[] postalAddress, string[] geographicAddress, string[] vanityAddress, ReformattedAddressResult? reformattedAddressResult, string[] reformattedAddress, double distance, Model.Link[] links)
        {
            if (postalAddress == null || postalAddress.Length == 0) throw new ArgumentNullException("postalAddress");
            if (distance < -0.00001D) throw new ArgumentNullException("distance");
            if (links == null) throw new ArgumentNullException("links");

            AddressId = addressId;
            PostalAddress = postalAddress;
            GeographicAddress = geographicAddress;
            VanityAddress = vanityAddress;
            ReformattedAddressResult = reformattedAddressResult;
            ReformattedAddress = reformattedAddress;
            Distance = distance;

            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "getEcadData":
                        newLink = new GetEcadData.Link(link.Rel, link.Href);
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
        /// Gets the Address ID (i.e. ECAD ID for Ireland).
        /// </summary>
        public int AddressId { get; private set; }

        /// <summary>
        /// Gets the postal address for the building in requested language.
        /// </summary>
        public string[] PostalAddress { get; private set; }

        /// <summary>
        /// Gets the geographic address for the building in requested language.
        /// </summary>
        public string[] GeographicAddress { get; private set; }

        /// <summary>
        /// Gets the vanity address (if requested).
        /// </summary>
        public string[] VanityAddress { get; private set; }

        /// <summary>
        /// Gets the reformatted address result.
        /// </summary>
        public ReformattedAddressResult? ReformattedAddressResult { get; private set; }

        /// <summary>
        /// Gets the address reformatted (if an address profile name supplied in request).
        /// </summary>
        public string[] ReformattedAddress { get; private set; }

        /// <summary>
        /// Get the distance of building from location in metres.
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Gets the links.
        /// </summary>
        public Model.Link[] Links { get; private set; }
    }
}