using System;
using Autoaddress.Autoaddress2_0.Extensions;

namespace Autoaddress.Autoaddress2_0.Model.ReverseGeocode
{
    /// <summary>
    /// Link returned in a ReverseGeocode Response
    /// </summary>
    public class Link : Model.Link
    {
        internal Link(string rel, Uri href)
            : base(rel, href)
        {
            Rel = rel;
            Href = href.RemovePort();
        }
    }
}