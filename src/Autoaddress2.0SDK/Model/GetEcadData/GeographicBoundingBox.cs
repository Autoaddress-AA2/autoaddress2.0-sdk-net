using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Geographic Bounding Box
    /// </summary>
    public class GeographicBoundingBox
    {
        [JsonConstructor]
        internal GeographicBoundingBox(GeographicCoordinates min, GeographicCoordinates max)
        {
            if (min == null) throw new ArgumentNullException("min");
            if (max == null) throw new ArgumentNullException("max");
            
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Gets the minimum geographic coordinates.
        /// </summary>
        public GeographicCoordinates Min { get; private set; }
        
        /// <summary>
        /// Gets the maximum geographic coordinates.
        /// </summary>
        public GeographicCoordinates Max { get; private set; }
    }
}