namespace Autoaddress.Autoaddress2_0.Model.MapId
{
    /// <summary>
    /// Result of a MapId call
    /// </summary>
    public enum ReturnCode
    {
        Unknown,
        EcadIdValid = 100,
        EcadIdInvalid = 200,
        GeoDirectoryIdValid = 300,
        GeoDirectoryIdInvalid = 400
    }
}