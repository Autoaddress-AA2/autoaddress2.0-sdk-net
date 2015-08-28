namespace Autoaddress.Autoaddress2_0.Model.VerifyAddress
{
    /// <summary>
    /// Result of a VerifyAddress call
    /// </summary>
    public enum ReturnCode
    {
        Unknown,
        AddressAndEircodeMatch = 100,
        AddressAndEircodeNoMatch = 200
    }
}