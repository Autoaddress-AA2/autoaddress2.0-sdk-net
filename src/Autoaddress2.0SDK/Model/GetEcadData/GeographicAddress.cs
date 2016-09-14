using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Geographic Address
    /// </summary>
    public class GeographicAddress
    {
        [JsonConstructor]
        internal GeographicAddress(string[] english, string[] irish)
        {
            if (english == null) throw new ArgumentNullException("english");
            if (irish == null) throw new ArgumentNullException("irish");
            
            English = english;
            Irish = irish;
        }

        /// <summary>
        /// Gets the English geographic address.
        /// </summary>
        public string[] English { get; private set; }

        /// <summary>
        /// Gets the Irish geographic address.
        /// </summary>
        public string[] Irish { get; private set; }
    }
}