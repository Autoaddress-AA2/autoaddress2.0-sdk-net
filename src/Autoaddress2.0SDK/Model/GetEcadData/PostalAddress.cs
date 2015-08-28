using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Postal Address
    /// </summary>
    public class PostalAddress
    {
        [JsonConstructor]
        internal PostalAddress(string[] english, string[] irish)
        {
            if (english == null) throw new ArgumentNullException("english");
            if (irish == null) throw new ArgumentNullException("irish");
            
            English = english;
            Irish = irish;
        }

        /// <summary>
        /// Gets the English postal address.
        /// </summary>
        public string[] English { get; private set; }
        
        /// <summary>
        /// Gets the Irish postal address.
        /// </summary>
        public string[] Irish { get; private set; }
    }
}