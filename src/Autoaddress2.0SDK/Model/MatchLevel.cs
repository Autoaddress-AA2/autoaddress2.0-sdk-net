namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// Match Level
    /// </summary>
    public enum MatchLevel
    {
#pragma warning disable CS1591  //  Missing XML comment for publicly visible type or member
        Unknown = 0,
#pragma warning restore CS1591  //  Missing XML comment for publicly visible type or member
        /// <summary>
        /// An address within a building which has no underlying address information.
        /// </summary>
        SubAddressPoint = 1,
        /// <summary>
        /// A unique address within a building.
        /// </summary>
        AddressPoint = 2,
        /// <summary>
        /// A non-residential address.
        /// </summary>
        Organisation = 3,
        /// <summary>
        /// A building.
        /// </summary>
        Building = 4,
        /// <summary>
        /// A collection of buildings with a collective name, located on or near the same thoroughfare
        /// </summary>
        BuildingGroup = 5,
        /// <summary>
        /// Refers to a street, road, avenue, etc.
        /// </summary>
        Thoroughfare = 6,
        /// <summary>
        /// Refers to areas, districts, towns, etc.
        /// </summary>
        Locality = 7,
        /// <summary>
        /// Post Town
        /// </summary>
        PostTown = 8,
        /// <summary>
        /// County
        /// </summary>
        County = 9
    }
}