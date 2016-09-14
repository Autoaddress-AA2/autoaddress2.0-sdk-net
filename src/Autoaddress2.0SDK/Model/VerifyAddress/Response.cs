using System;
using System.Collections.Generic;
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
                          string[] geographicAddress,
                          string[] vanityAddress,
                          string postcode,
                          int? addressId,
                          AddressType? addressType,
                          MatchLevel? matchLevel,
                          Request input,
                          Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links");

            Result = result;
            PostalAddress = postalAddress;
            GeographicAddress = geographicAddress;
            VanityAddress = vanityAddress;
            Postcode = postcode;
            AddressId = addressId;
            AddressType = addressType;
            MatchLevel = matchLevel;
            Input = input;
            
            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "self":
                        newLink = new Model.VerifyAddress.Link(link.Rel, link.Href);
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
        /// Gets the result of the search.
        /// </summary>
        public ReturnCode Result { get; private set; }
        
        /// <summary>
        /// Gets the postal address in requested language for the input address.
        /// </summary>
        public string[] PostalAddress { get; private set; }

        /// <summary>
        /// Gets the geographic address in requested language for the input address.
        /// </summary>
        public string[] GeographicAddress { get; private set; }

        /// <summary>
        /// Vanity address in case it was requested per input and it is available.
        /// </summary>
        public string[] VanityAddress { get; private set; }

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
        public Model.Link[] Links { get; private set; }
    }
}