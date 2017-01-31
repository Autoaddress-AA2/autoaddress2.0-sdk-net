namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Ecad ID Status
    /// </summary>
    public enum EcadIdStatus
    {
        Unknown,
        Current = 100,
        Changed = 110,
        Retired = 120,
        Invalid = 200
    }
}