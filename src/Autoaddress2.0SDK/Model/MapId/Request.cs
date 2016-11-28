namespace Autoaddress.Autoaddress2_0.Model.MapId
{
    /// <summary>
    /// Container for parameters of MapId
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="ecadId">The ECAD ID to get the GeoDirectory ID for.</param>
        /// <param name="geoDirectoryId">The GeoDirectory ID to get the ECAD ID for.</param>
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        public Request(int? ecadId = null, string geoDirectoryId = null, string txn = null)
        {
            EcadId = ecadId;
            GeoDirectoryId = geoDirectoryId;
            Txn = txn;
        }

        /// <summary>
        /// Gets the ECAD ID.
        /// </summary>
        public int? EcadId { get; private set; }

        /// <summary>
        /// Gets the GeoDirectory ID.
        /// </summary>
        public string GeoDirectoryId { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }
    }
}