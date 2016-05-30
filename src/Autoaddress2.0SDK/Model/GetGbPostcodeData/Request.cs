namespace Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData
{
    /// <summary>
    /// Container for parameters of GetGbPostcodeData
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="postcode">The postcode to get the data for.</param>
        /// <param name="txn">Transaction. If null then automatically assigned a value in associated response.</param>
        public Request(string postcode, string txn = null)
        {
            Postcode = postcode;
            Txn = txn;
        }

        /// <summary>
        /// Gets the postcode.
        /// </summary>
        public string Postcode { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public string Txn { get; private set; }
    }
}