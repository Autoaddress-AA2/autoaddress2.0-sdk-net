using System;
using Autoaddress.Autoaddress2_0.Extensions;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// Link returned in a AutoComplete Response
    /// </summary>
    public class Link
    {
        [JsonConstructor]
        internal Link(string rel, Uri href)
        {
            Rel = rel;
            Href = href.RemovePort();
        }

        /// <summary>
        /// Gets the rel.
        /// </summary>
        public string Rel { get; protected set; }

        /// <summary>
        /// Gets the href.
        /// </summary>
        public Uri Href { get; protected set; }
    }
}
