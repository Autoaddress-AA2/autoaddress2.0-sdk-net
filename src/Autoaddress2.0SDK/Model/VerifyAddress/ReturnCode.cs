namespace Autoaddress.Autoaddress2_0.Model.VerifyAddress
{
    /// <summary>
    /// Result of a VerifyAddress call
    /// </summary>
    public enum ReturnCode
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        AddressAndEircodeMatch = 100,
        AddressAndEircodeNoMatch = 200
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}