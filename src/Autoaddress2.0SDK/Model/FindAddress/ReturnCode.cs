namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Result of a FindAddress call
    /// </summary>
    public enum ReturnCode
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        PostcodeAppended = 100,
        PostcodeValidated = 110,
        PostcodeAmended = 120,
        AddressAmendedToMatchPostcode = 130,
        PostcodeAndAddressAmended = 140,
        PostcodeNotValidated = 150,
        PostcodeNotAvailable = 200,
        PostcodeRetired = 210,
        NonUniqueAddress = 300,
        PartialAddressMatch = 400,
        IncompleteAddressEntered = 500,
        NoAddressMatch = 550,
        ForeignAddressDetected = 600,
        InvalidAddressEntered = 700
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}