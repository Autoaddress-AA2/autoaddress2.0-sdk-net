using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Spatial Info
    /// </summary>
    public class SpatialInfo
    {
        [JsonConstructor]
        internal SpatialInfo(int ecadId,
                           ProjectedSpatialInfo ing,
                           ProjectedSpatialInfo itm,
                           GeographicSpatialInfo etrs89,
                           string spatialAccuracy)
        {
            EcadId = ecadId;
            Ing = ing;
            Itm = itm;
            Etrs89 = etrs89;
            SpatialAccuracy = spatialAccuracy;
        }

        /// <summary>
        /// Gets the ECAD Id.
        /// </summary>
        public int EcadId { get; private set; }

        /// <summary>
        /// Gets the ING spatial info.
        /// </summary>
        public ProjectedSpatialInfo Ing { get; private set; }
        
        /// <summary>
        /// Gets the ITM spatial info.
        /// </summary>
        public ProjectedSpatialInfo Itm { get; private set; }

        /// <summary>
        /// Gets the ETRS89 spatial info.
        /// </summary>
        public GeographicSpatialInfo Etrs89 { get; private set; }

        /// <summary>
        /// Gets the spatial accuracy.
        /// </summary>
        public string SpatialAccuracy { get; private set; }
    }
}