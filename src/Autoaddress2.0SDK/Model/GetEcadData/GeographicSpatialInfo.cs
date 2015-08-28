using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Geographic Spatial Info
    /// </summary>
    public class GeographicSpatialInfo
    {
        [JsonConstructor]
        internal GeographicSpatialInfo(GeographicCoordinates location, GeographicBoundingBox boundingBox)
        {
            Location = location;
            BoundingBox = boundingBox;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        public GeographicCoordinates Location { get; private set; }
        
        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        public GeographicBoundingBox BoundingBox { get; private set; }
    }
}