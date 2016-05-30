using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData
{
    /// <summary>
    /// Spatial Info
    /// </summary>
    public class SpatialInfo
    {
        [JsonConstructor]
        internal SpatialInfo(GbPostcodeGeographicSpatialInfo wgs84)
        {
            Wgs84 = wgs84;
        }

        /// <summary>
        /// Gets the WGS84 spatial info.
        /// </summary>
        public GbPostcodeGeographicSpatialInfo Wgs84 { get; private set; }
    }
}