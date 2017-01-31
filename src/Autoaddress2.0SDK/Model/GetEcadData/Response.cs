using System;
using System.Collections.Generic;
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
                          EcadIdStatus ecadIdStatus,
                          int? addressTypeId,
                          EircodeInfo eircodeInfo,
                          PostalAddress postalAddress,
                          GeographicAddress geographicAddress,
                          AdministrativeInfo administrativeInfo,
                          BuildingInfo buildingInfo,
                          OrganisationInfo organisationInfo,
                          SpatialInfo spatialInfo,
                          RelatedEcadIds relatedEcadIds,
                          Request input,
                          Model.Link[] links)
        {
            if (links == null) throw new ArgumentNullException("links");

            Result = result;
            EcadId = ecadId;
            EcadIdStatus = ecadIdStatus;
            AddressTypeId = addressTypeId;
            EircodeInfo = eircodeInfo;
            GeographicAddress = geographicAddress;
            PostalAddress = postalAddress;
            AdministrativeInfo = administrativeInfo;
            BuildingInfo = buildingInfo;
            OrganisationInfo = organisationInfo;
            SpatialInfo = spatialInfo;
            RelatedEcadIds = relatedEcadIds;
            Input = input;
            
            var newLinks = new List<Model.Link>();

            foreach (Model.Link link in links)
            {
                Model.Link newLink;

                switch (link.Rel)
                {
                    case "self":
                        newLink = new Model.GetEcadData.Link(link.Rel, link.Href);
                        break;
                    default:
                        newLink = link;
                        break;
                }

                newLinks.Add(newLink);
            }

            Links = newLinks.ToArray();
        }

        /// <summary>
        /// Gets the result of the lookup.
        /// </summary>
        public ReturnCode Result { get; private set; }

        /// <summary>
        /// Gets the ECAD ID.
        /// </summary>
        public int? EcadId { get; private set; }

        /// <summary>
        /// Gets the ECAD ID status.
        /// </summary>
        public EcadIdStatus EcadIdStatus { get; private set; }

        /// <summary>
        /// Gets the type of address found.
        /// </summary>
        public int? AddressTypeId { get; private set; }

        /// <summary>
        /// Gets the Eircode info.
        /// </summary>
        public EircodeInfo EircodeInfo { get; private set; }

        /// <summary>
        /// Gets the postal address in requested language.
        /// </summary>
        public PostalAddress PostalAddress { get; private set; }

        /// <summary>
        /// Gets the geographic address in requested language.
        /// </summary>
        public GeographicAddress GeographicAddress { get; private set; }

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
        /// Gets the related ECAD ids.
        /// </summary>
        public RelatedEcadIds RelatedEcadIds { get; private set; }

        /// <summary>
        /// Gets the input request.
        /// </summary>
        public Request Input { get; private set; }

        /// <summary>
        /// Gets an array of Link objects.
        /// </summary>
        public Model.Link[] Links { get; set; }
    }
}