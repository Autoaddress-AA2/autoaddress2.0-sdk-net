namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Container for parameters of GetEcadData
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="ecadId">The ECAD id to get the ECAD data for.</param>
        /// <param name="history">Whether or not to return history.</param>
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        /// <param name="administrativeInfo">Release of AdministrativeInfo to return. For example "2015" or "2017".</param>
        public Request(int ecadId, bool history, string txn = null, string administrativeInfo = null)
        {
            EcadId = ecadId;
            History = history;
            Txn = txn;
            AdministrativeInfo = administrativeInfo;
        }

        /// <summary>
        /// Gets the ECAD id.
        /// </summary>
        public int EcadId { get; private set; }

        /// <summary>
        /// Gets whether or not to return history.
        /// </summary>
        public bool History { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }

        /// <summary>
        /// Gets the AdministrativeInfo release.
        /// </summary>
        public string AdministrativeInfo { get; private set; }
    }
}