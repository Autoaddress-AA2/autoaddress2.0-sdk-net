using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Eircode Info
    /// </summary>
    public class EircodeInfo
    {
        [JsonConstructor]
        internal EircodeInfo(int ecadId,
                             string eircode)
        {
            EcadId = ecadId;
            Eircode = eircode;
        }

        /// <summary>
        /// Gets the ECAD Id of the address point that the Eircode corresponds to.
        /// </summary>
        public int EcadId { get; private set; }
        
        /// <summary>
        /// Gets the Eircode.
        /// </summary>
        public string Eircode { get; private set; }
    }
}