﻿namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// Ecad ID Status
    /// </summary>
    public enum EcadIdStatus
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown,
        Current = 100,
        Changed = 110,
        Retired = 120,
        Invalid = 200
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}