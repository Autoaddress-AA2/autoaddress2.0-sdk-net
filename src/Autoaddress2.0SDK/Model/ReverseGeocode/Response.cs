using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.ReverseGeocode
{
    /// <summary>
    /// ReverseGeocode response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(Hit[] hits,
                          Request input,
                          Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links"); 

            Hits = hits;
            Input = input;
            Links = links;

            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "self":
                        newLink = new Model.ReverseGeocode.Link(link.Rel, link.Href);
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
        /// Gets an array of Hit objects.
        /// </summary>
        public Hit[] Hits { get; private set; }

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