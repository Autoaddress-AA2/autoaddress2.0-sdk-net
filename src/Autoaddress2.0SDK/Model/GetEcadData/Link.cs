using System;
using Autoaddress.Autoaddress2_0.Extensions;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Link returned in a GetEcadData Response
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
        /// Gets or sets the rel.
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        public Uri Href { get; set; }
    }
}
