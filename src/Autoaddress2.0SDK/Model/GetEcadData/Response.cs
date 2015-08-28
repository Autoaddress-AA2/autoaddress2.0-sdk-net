using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.GetEcadData
{
    /// <summary>
    /// GetEcadData response
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        internal Response(ReturnCode result,
                        int? ecadId,
                        int? addressTypeId,
                        PostalAddress postalAddress,
                        AdministrativeInfo administrativeInfo,
                        BuildingInfo buildingInfo,
                        OrganisationInfo organisationInfo,
                        SpatialInfo spatialInfo,
                        Request input,
                        Link[] links)
        {
            Result = result;
            EcadId = ecadId;
            AddressTypeId = addressTypeId;
            PostalAddress = postalAddress;
            AdministrativeInfo = administrativeInfo;
            BuildingInfo = buildingInfo;
            OrganisationInfo = organisationInfo;
            SpatialInfo = spatialInfo;
            Input = input;
            Links = links;
        }

        /// <summary>
        /// Gets the result of the lookup.
        /// </summary>
        public ReturnCode Result { get; private set; }

        /// <summary>
        /// Gets the ECAD id.
        /// </summary>
        public int? EcadId { get; private set; }
        
        /// <summary>
        /// Gets the type of address found.
        /// </summary>
        public int? AddressTypeId { get; private set; }
        
        /// <summary>
        /// Gets the postal address in requested language.
        /// </summary>
        public PostalAddress PostalAddress { get; private set; }

        /// <summary>
        /// Gets the administrative info.
        /// </summary>
        public AdministrativeInfo AdministrativeInfo { get; private set; }

        /// <summary>
        /// Gets the building info.
        /// </summary>
        public BuildingInfo BuildingInfo { get; private set; }
        
        /// <summary>
        /// Gets the organisation info.
        /// </summary>
        public OrganisationInfo OrganisationInfo { get; private set; }
        
        /// <summary>
        /// Gets the spatial info.
        /// </summary>
        public SpatialInfo SpatialInfo { get; private set; }
        
        /// <summary>
        /// Gets the input request.
        /// </summary>
        public Request Input { get; private set; }
        
        /// <summary>
        /// Gets an array of Link objects.
        /// </summary>
        public Link[] Links { get; set; }
    }
}