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
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        public Request(int ecadId, string txn = null)
        {
            EcadId = ecadId;
            Txn = txn;
        }

        /// <summary>
        /// Gets the ECAD id.
        /// </summary>
        public int EcadId { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }
    }
}