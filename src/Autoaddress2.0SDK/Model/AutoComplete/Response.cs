using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.AutoComplete
{
    /// <summary>
    /// AutoComplete response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(int totalOptions, Option[] options, Request input, Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links");

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
                        newLink = new Model.AutoComplete.Link(link.Rel, link.Href);
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
        /// Gets the number of options found.
        /// </summary>
        public int TotalOptions { get; set; }

        /// <summary>
        /// Gets an array of Option objects.
        /// </summary>
        public Option[] Options { get; set; }
        
        /// <summary>
        /// Gets the input request.
        /// </summary>
        public Request Input { get; set; }
        
        /// <summary>
        /// Gets an array of Link objects.
        /// </summary>
        public Model.Link[] Links { get; set; }
    }
}
