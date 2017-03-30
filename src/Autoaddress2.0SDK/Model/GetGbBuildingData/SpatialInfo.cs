using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetGbBuildingData
{
    /// <summary>
    /// Spatial Info
    /// </summary>
    public class SpatialInfo
    {
        [JsonConstructor]
        internal SpatialInfo(GbGeographicSpatialInfo wgs84)
        {
            Wgs84 = wgs84;
        }

        /// <summary>
        /// Gets the WGS84 spatial info.
        /// </summary>
        public GbGeographicSpatialInfo Wgs84 { get; private set; }
    }
}