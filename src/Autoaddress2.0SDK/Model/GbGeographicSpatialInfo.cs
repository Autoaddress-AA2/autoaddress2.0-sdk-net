using Autoaddress.Autoaddress2_0.Model.GetEcadData;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// GbGeographicSpatialInfo
    /// </summary>
    public class GbGeographicSpatialInfo
    {
        [JsonConstructor]
        internal GbGeographicSpatialInfo(GeographicCoordinates location)
        {
            Location = location;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        public GeographicCoordinates Location { get; private set; }
    }
}