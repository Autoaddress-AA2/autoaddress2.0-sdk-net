using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Geographic Coordinates
    /// </summary>
    public class GeographicCoordinates
    {
        [JsonConstructor]
        internal GeographicCoordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude { get; private set; }
    }
}