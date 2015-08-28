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
        public Request(int ecadId)
        {
            EcadId = ecadId;
        }

        /// <summary>
        /// Gets the ECAD id.
        /// </summary>
        public int EcadId { get; private set; }
    }
}