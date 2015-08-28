using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Organisation Info
    /// </summary>
    public class OrganisationInfo
    {
        [JsonConstructor]
        internal OrganisationInfo(int ecadId, string naceCode, string naceCategory, bool? vacant)
        {
            EcadId = ecadId;
            NaceCode = naceCode;
            NaceCategory = naceCategory;
            Vacant = vacant;
        }

        /// <summary>
        /// Gets the ECAD Id.
        /// </summary>
        public int EcadId { get; private set; }
        
        /// <summary>
        /// Gets the NACE code.
        /// </summary>
        public string NaceCode { get; private set; }
        
        /// <summary>
        /// Gets the NACE category.
        /// </summary>
        public string NaceCategory { get; private set; }
        
        /// <summary>
        /// Gets whether or not the organisation is vacant.
        /// </summary>
        public bool? Vacant { get; private set; }
    }
}