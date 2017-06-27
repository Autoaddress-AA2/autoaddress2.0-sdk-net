using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// FindAddress response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(ReturnCode result,
                          bool? isUniqueAddress,
                          string postcode,
                          int? addressId,
                          AddressType? addressType,
                          MatchLevel matchLevel,
                          string[] unmatched,
                          AddressElement[] unmatchedAddressElements,
                          string[] postalAddress,
                          AddressElement[] postalAddressElements,
                          string[] geographicAddress,
                          AddressElement[] geographicAddressElements,
                          string[] vanityAddress,
                          AddressElement[] vanityAddressElements,
                          ReformattedAddressResult? reformattedAddressResult,
                          string[] reformattedAddress,
                          int totalOptions,
                          Option[] options,
                          Request input,
                          Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links");

            Result = result;
            IsUniqueAddress = isUniqueAddress;
            Postcode = postcode;
            AddressId = addressId;
            AddressType = addressType;
            MatchLevel = matchLevel;
            PostalAddress = postalAddress;
            PostalAddressElements = postalAddressElements;
            GeographicAddress = geographicAddress;
            GeographicAddressElements = geographicAddressElements;
            VanityAddress = vanityAddress;
            VanityAddressElements = vanityAddressElements;
            ReformattedAddressResult = reformattedAddressResult;
            ReformattedAddress = reformattedAddress;
            Unmatched = unmatched;
            UnmatchedAddressElements = unmatchedAddressElements;
            TotalOptions = totalOptions;
            Options = options;
            Input = input;
            
            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "self":
                        newLink = new Model.FindAddress.Link(link.Rel, link.Href);
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
        /// Gets whether or not the address is unique.
        /// </summary>
        public bool? IsUniqueAddress { get; private set; }

        /// <summary>
        /// Gets the Eircode or postcode.
        /// </summary>
        public string Postcode { get; private set; }

        /// <summary>
        /// Gets the Address ID (i.e. ECAD ID for Ireland).
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
        /// Gets the part of the input address that could not be matched.
        /// </summary>
        public string[] Unmatched { get; private set; }

        /// <summary>
        /// Gets the unmatched address elements.
        /// </summary>
        public AddressElement[] UnmatchedAddressElements { get; private set; }

        /// <summary>
        /// Gets the postal address in requested language for the input address.
        /// </summary>
        public string[] PostalAddress { get; private set; }

        /// <summary>
        /// Gets the postal address elements.
        /// </summary>
        public AddressElement[] PostalAddressElements { get; private set; }

        /// <summary>
        /// Gets the geographic address in requested language for the input address.
        /// </summary>
        public string[] GeographicAddress { get; private set; }

        /// <summary>
        /// Gets the geographic address elements.
        /// </summary>
        public AddressElement[] GeographicAddressElements { get; private set; }

        /// <summary>
        /// Gets the vanity address (if requested).
        /// </summary>
        public string[] VanityAddress { get; private set; }

        /// <summary>
        /// Gets the vanity address elements.
        /// </summary>
        public AddressElement[] VanityAddressElements { get; private set; }

        /// <summary>
        /// Gets the reformatted address result.
        /// </summary>
        public ReformattedAddressResult? ReformattedAddressResult { get; private set; }

        /// <summary>
        /// Gets the address reformatted (if an address profile name supplied in request).
        /// </summary>
        public string[] ReformattedAddress { get; private set; }
        
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
        public Model.Link[] Links { get; private set; }
    }
}