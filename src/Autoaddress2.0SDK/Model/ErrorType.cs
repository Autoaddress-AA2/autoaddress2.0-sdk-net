namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// Types of errors returned by the Autoaddress 2.0 service
    /// </summary>
    public enum ErrorType
    {
        Unknown = 0,
        MissingLicenceKey = 401001,
        Unauthorized = 403,
        InvalidLicenceKey = 401002,
        LicenceKeyDisabled = 401003,
        LicenceKeyExpired = 401004,
        AccessToRestrictedData = 401005,
        InvalidLanguage = 400001,
        InvalidCountry = 400002,
        InvalidLimit = 400003,
        InvalidAddressId = 400004,
        InvalidEcadId = 400005,
        InvalidSmartGroupingIndex = 400006,
        InvalidCombinationOfCountryAndLanguage = 400007,
        MissingAddress = 400008,
        MissingPostcode = 400009,
        MissingEcadId = 400010,
        NotFound = 404001,
        InternalServerError = 500000
    }
}
