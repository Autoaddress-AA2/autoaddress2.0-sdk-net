using System;
using Autoaddress.Autoaddress2_0.Extensions;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Link returned in a GetEcadData Response
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