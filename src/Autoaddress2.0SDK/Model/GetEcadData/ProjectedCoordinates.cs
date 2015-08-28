using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Projected Coordinates
    /// </summary>
    public class ProjectedCoordinates
    {
        [JsonConstructor]
        internal ProjectedCoordinates(double easting, double northing)
        {
            Easting = easting;
            Northing = northing;
        }

        /// <summary>
        /// Gets the easting.
        /// </summary>
        public double Easting { get; private set; }
        
        /// <summary>
        /// Gets the northing.
        /// </summary>
        public double Northing { get; private set; }
    }
}