using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Projected Spatial Info
    /// </summary>
    public class ProjectedSpatialInfo
    {
        [JsonConstructor]
        internal ProjectedSpatialInfo(ProjectedCoordinates location, ProjectedBoundingBox boundingBox)
        {
            Location = location;
            BoundingBox = boundingBox;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        public ProjectedCoordinates Location { get; private set; }
        
        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        public ProjectedBoundingBox BoundingBox { get; private set; }
    }
}