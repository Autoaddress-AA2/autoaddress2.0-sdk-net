using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Related Ecad Ids
    /// </summary>
    public class RelatedEcadIds
    {
        [JsonConstructor]
        internal RelatedEcadIds(int[] geographicCountyEcadIds)
        {
            GeographicCountyEcadIds = geographicCountyEcadIds;
        }

        /// <summary>
        /// Gets the geographic county ECAD ids.
        /// </summary>
        public int[] GeographicCountyEcadIds { get; private set; }
    }
}