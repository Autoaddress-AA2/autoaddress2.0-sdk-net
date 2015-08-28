namespace Autoaddress.Autoaddress2_0.Model.PostcodeLookup
{
    /// <summary>
    /// Result of a FindAddress call
    /// </summary>
    public enum ReturnCode
    {
        Unknown,
        ValidPostcode = 100, 
        InvalidPostcode = 200, 
        PartialPostcode = 150
    }
}