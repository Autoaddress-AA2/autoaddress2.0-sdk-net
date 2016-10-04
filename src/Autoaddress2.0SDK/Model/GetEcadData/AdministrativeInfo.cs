using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Administrative Info
    /// </summary>
    public class AdministrativeInfo
    {
        [JsonConstructor]
        internal AdministrativeInfo(int ecadId,
                                  int? laId,
                                  int? dedId,
                                  int? smallAreaId,
                                  int? townlandId,
                                  int? countyId,
                                  bool? gaeltacht)
        {
            EcadId = ecadId;
            LaId = laId;
            DedId = dedId;
            SmallAreaId = smallAreaId;
            TownlandId = townlandId;
            CountyId = countyId;
            Gaeltacht = gaeltacht;
        }

        /// <summary>
        /// Gets the ECAD Id that the Administrative Info corresponds to.
        /// </summary>
        public int EcadId { get; private set; }
        
        /// <summary>
        /// Gets the Local Authority Id.
        /// </summary>
        public int? LaId { get; private set; }
        
        /// <summary>
        /// Gets the Electoral District Id.
        /// </summary>
        public int? DedId { get; private set; }
        
        /// <summary>
        /// Gets the Small Area Id.
        /// </summary>
        public int? SmallAreaId { get; private set; }
        
        /// <summary>
        /// Gets the Townland Id.
        /// </summary>
        public int? TownlandId { get; private set; }

        /// <summary>
        /// Gets the County Id.
        /// </summary>
        public int? CountyId { get; private set; }

        /// <summary>
        /// Gets whether or not Ecad Id is in a Gaeltacht area.
        /// </summary>
        public bool? Gaeltacht { get; private set; }
    }
}