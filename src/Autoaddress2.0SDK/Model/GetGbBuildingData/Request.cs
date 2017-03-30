namespace Autoaddress.Autoaddress2_0.Model.GetGbBuildingData
{
    /// <summary>
    /// Container for parameters of GetGbBuildingData
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="addressId">The address ID to get the data for.</param>
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        public Request(string addressId, string txn = null)
        {
            AddressId = addressId;
            Txn = txn;
        }

        /// <summary>
        /// Gets the address ID.
        /// </summary>
        public string AddressId { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }
    }
}