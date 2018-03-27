namespace Autoaddress.Autoaddress2_0.Model.MapId
{
    /// <summary>
    /// Result of a MapId call
    /// </summary>
    public enum ReturnCode
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        EcadIdValid = 100,
        EcadIdInvalid = 200,
        GeoDirectoryIdValid = 300,
        GeoDirectoryIdInvalid = 400
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}