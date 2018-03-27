namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Reasons why ReturnCode is PostcodeNotAvailable
    /// </summary>
    public enum PostcodeNotAvailable
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        NoMailDelivery = 1,
        NoRoutingKey = 2,
        NoCoordinates = 3
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
    }
}