namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Result of a FindAddress call
    /// </summary>
    public enum ReturnCode
    {
        Unknown,
        ValidPostcode = 100,
        RetiredPostcode = 110,
        ChangedPostcode = 120,
        PartialPostcode = 150,
        InvalidPostcode = 200
    }
}