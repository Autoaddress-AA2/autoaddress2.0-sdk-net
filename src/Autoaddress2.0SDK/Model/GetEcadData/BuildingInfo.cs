using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Building Info
    /// </summary>
    public class BuildingInfo
    {
        [JsonConstructor]
        internal BuildingInfo(int ecadId,
                              int? buildingTypeId,
                              bool? holidayHome,
                              bool? underConstruction,
                              string buildingUse,
                              bool? vacant)
        {
            EcadId = ecadId;
            BuildingTypeId = buildingTypeId;
            HolidayHome = holidayHome;
            UnderConstruction = underConstruction;
            BuildingUse = buildingUse;
            Vacant = vacant;
        }

        /// <summary>
        /// Gets the building ECAD Id that the Building Info corresponds to.
        /// </summary>
        public int EcadId { get; private set; }
        
        /// <summary>
        /// Gets the building type id.
        /// </summary>
        public int? BuildingTypeId { get; private set; }
        
        /// <summary>
        /// Gets whether or not the building is a holiday home.
        /// </summary>
        public bool? HolidayHome { get; private set; }

        /// <summary>
        /// Gets whether or not the building is under construction.
        /// </summary>
        public bool? UnderConstruction { get; private set; }

        /// <summary>
        /// Gets the building use.
        /// </summary>
        public string BuildingUse { get; private set; }
        
        /// <summary>
        /// Gets whether or not the building is vacant.
        /// </summary>
        public bool? Vacant { get; private set; }
    }
}