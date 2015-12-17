using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Related Ecad Ids
    /// </summary>
    public class RelatedEcadIds
    {
        [JsonConstructor]
        internal RelatedEcadIds(int? addressPointEcadId,
                                int? buildingEcadId,
                                int? buildingGroupEcadId,
                                int[] thoroughfareEcadIds,
                                int[] localityEcadIds,
                                int[] postTownEcadIds,
                                int[] postCountyEcadIds,
                                int[] geographicCountyEcadIds)
        {
            AddressPointEcadId = addressPointEcadId;
            BuildingEcadId = buildingEcadId;
            BuildingGroupEcadId = buildingGroupEcadId;
            ThoroughfareEcadIds = thoroughfareEcadIds;
            LocalityEcadIds = localityEcadIds;
            PostTownEcadIds = postTownEcadIds;
            PostCountyEcadIds = postCountyEcadIds;
            GeographicCountyEcadIds = geographicCountyEcadIds;
        }
        
        /// <summary>
        /// Gets the address point ECAD id.
        /// </summary>
        public int? AddressPointEcadId { get; private set; }
        
        /// <summary>
        /// Gets the building ECAD id.
        /// </summary>
        public int? BuildingEcadId { get; private set; }

        /// <summary>
        /// Gets the building group ECAD ids.
        /// </summary>
        public int? BuildingGroupEcadId { get; private set; }

        /// <summary>
        /// Gets the thoroughfare ECAD ids.
        /// </summary>
        public int[] ThoroughfareEcadIds { get; private set; }
        
        /// <summary>
        /// Gets the locality ECAD ids.
        /// </summary>
        public int[] LocalityEcadIds { get; private set; }

        /// <summary>
        /// Gets the post town ECAD ids.
        /// </summary>
        public int[] PostTownEcadIds { get; private set; }

        /// <summary>
        /// Gets the post county ECAD ids.
        /// </summary>
        public int[] PostCountyEcadIds { get; private set; }

        /// <summary>
        /// Gets the geographic county ECAD ids.
        /// </summary>
        public int[] GeographicCountyEcadIds { get; private set; }
    }
}