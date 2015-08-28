using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Projected Bounding Box
    /// </summary>
    public class ProjectedBoundingBox
    {
        [JsonConstructor]
        internal ProjectedBoundingBox(ProjectedCoordinates min, ProjectedCoordinates max)
        {
            if (min == null) throw new ArgumentNullException("min");
            if (max == null) throw new ArgumentNullException("max");
            
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Gets the minimum projected coordinates.
        /// </summary>
        public ProjectedCoordinates Min { get; private set; }

        /// <summary>
        /// Gets the maximum projected coordinates.
        /// </summary>
        public ProjectedCoordinates Max { get; private set; }
    }
}