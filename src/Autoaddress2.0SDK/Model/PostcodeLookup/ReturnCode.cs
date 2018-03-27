namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Result of a FindAddress call
    /// </summary>
    public enum ReturnCode
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        ValidPostcode = 100,
        RetiredPostcode = 110,
        ChangedPostcode = 120,
        PartialPostcode = 150,
        InvalidPostcode = 200
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}