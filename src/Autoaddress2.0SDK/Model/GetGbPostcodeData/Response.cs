using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData
{
    /// <summary>
    /// GetGbPostcodeData response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(ReturnCode result,
                          string postcode,
                          SpatialInfo spatialInfo,
                          Request input,
                          Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links");

            Result = result;
            Postcode = postcode;
            SpatialInfo = spatialInfo;
            Input = input;
            
            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "self":
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
        /// Gets the result of the lookup.
        /// </summary>
        public ReturnCode Result { get; private set; }

        /// <summary>
        /// Gets the postcode.
        /// </summary>
        public string Postcode { get; private set; }

        /// <summary>
        /// Gets the spatial info.
        /// </summary>
        public SpatialInfo SpatialInfo { get; private set; }

        /// <summary>
        /// Gets the input request.
        /// </summary>
        public Request Input { get; private set; }
        
        /// <summary>
        /// Gets an array of Link objects.
        /// </summary>
        public Model.Link[] Links { get; set; }
    }
}