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
        LimitExceeded = 401006,
        InvalidLanguage = 400001,
        InvalidCountry = 400002,
        InvalidLimit = 400003,
        InvalidAddressId = 400004,
        InvalidEcadId = 400005,
        InvalidSelectionIndex = 400006,
        InvalidCombinationOfCountryAndLanguage = 400007,
        MissingAddress = 400008,
        MissingPostcode = 400009,
        MissingEcadId = 400010,
        InvalidParameterValue = 400011,
        InvalidAddressProfileName = 400012,
        InvalidAbbreviateOption = 400013,
        InvalidCapitaliseOption = 400014,
        InvalidIrishLetterOption = 400015,
        InvalidGeographicAddress = 400016,
        InvalidVanityMode = 400017,
        InvalidAddressElements = 400018,
        InvalidTxn = 400019,
        InvalidAddress = 400020,
        InvalidPostcode = 400022,
        InvalidUuid = 400023,
        InvalidGeodirectoryId = 400024,
        InvalidRequestOriginator = 400025,
        InvalidGeodirectoryVersion = 400026,
        NotFound = 404001,
        InternalServerError = 500000
    }
}
