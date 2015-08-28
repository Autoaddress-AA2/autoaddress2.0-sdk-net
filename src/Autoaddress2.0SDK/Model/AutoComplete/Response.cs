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
        internal Response(int totalOptions, Option[] options, Request input, Link[] links)
        {
            TotalOptions = totalOptions;
            Options = options;
            Input = input;
            Links = links;
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
        public Link[] Links { get; set; }
    }
}
