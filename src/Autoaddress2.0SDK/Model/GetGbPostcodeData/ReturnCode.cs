namespace Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData
{
    /// <summary>
    /// Result of a GetGbPostcodeData call
    /// </summary>
    public enum ReturnCode
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        PostcodeValid = 100,
        PostcodeInvalid = 200
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}