namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Result of a FindAddress call
    /// </summary>
    public enum ReturnCode
    {
        Unknown,
        PostcodeAppended = 100,
        PostcodeValidated = 110,
        PostcodeAmended = 120,
        AddressAmendedToMatchPostcode = 130,
        PostcodeAndAddressAmended = 140,
        PostcodeNotAvailable = 200,
        PostcodeRetired = 210,
        NonUniqueAddress = 300,
        PartialAddressMatch = 400,
        IncompleteAddressEntered = 500,
        ForeignAddressDetected = 600,
        InvalidAddressEntered = 700
    }
}