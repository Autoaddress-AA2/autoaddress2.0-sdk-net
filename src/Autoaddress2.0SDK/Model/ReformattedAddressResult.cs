namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// Reformatted Address Result
    /// </summary>
    public enum ReformattedAddressResult
    {
        Unknown = 0,
        /// <summary>
        /// All address elements have been used.
        /// </summary>
        Success = 100,
        /// <summary>
        /// At least one address element has been truncated.
        /// </summary>
        AddressElementTruncated = 101,
        /// <summary>
        /// At least one address element has been lost.
        /// </summary>
        AddressElementLost = 102,
        /// <summary>
        /// At least one address element has been lost and at least one address element has been truncated.
        /// </summary>
        AddressElementLostAndTruncated = 103,
        /// <summary>
        /// All address elements have been used, but at least one has been abbreviated.
        /// </summary>
        AddressElementAbbreviated = 110
    }
}
