using Newtonsoft.Json;
using System;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Date info
    /// </summary>
    public class DateInfo
    {
        [JsonConstructor]
        internal DateInfo(DateTime? created, DateTime? modified)
        {
            Created = created;
            Modified = modified;
        }

        /// <summary>
        /// Gets date and time when this data was created
        /// </summary>
        public DateTime? Created { get; private set; }

        /// <summary>
        /// Gets date and time when this data was last modified
        /// </summary>
        public DateTime? Modified { get; private set; }
    }
}