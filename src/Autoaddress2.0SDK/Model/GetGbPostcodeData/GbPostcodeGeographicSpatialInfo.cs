using Autoaddress.Autoaddress2_0.Model.GetEcadData;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData
{
    /// <summary>
    /// Geographic Spatial Info
    /// </summary>
    public class GbPostcodeGeographicSpatialInfo
    {
        [JsonConstructor]
        internal GbPostcodeGeographicSpatialInfo(GeographicCoordinates location)
        {
            Location = location;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        public GeographicCoordinates Location { get; private set; }
    }
}