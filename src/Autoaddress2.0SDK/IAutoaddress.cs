﻿using System;
using System.Threading.Tasks;

namespace Autoaddress.Autoaddress2_0
{
    /// <summary>
    /// Interface for accessing the Autoaddress 2.0 service.
    /// </summary>
    public interface IAutoaddress
    {
        /// <summary>
        /// Occurs just before request is sent to Autoaddress endpoint.
        /// </summary>
        event EventHandler<PreRequestEventArgs> PreRequest;

        /// <summary>
        /// Occurs after response was received from Autoaddress endpoint.
        /// </summary>
        event EventHandler<PostRequestEventArgs> PostRequest;

        /// <summary>
        /// Lookup a Postcode or Address. Returns all available data if found.
        /// </summary>
        /// <param name="request">FindAddress request.</param>
        /// <returns>FindAddress response.</returns>
        Model.FindAddress.Response FindAddress(Model.FindAddress.Request request);

        /// <summary>
        /// Lookup a Postcode or Address. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a FindAddress response.</param>
        /// <returns>FindAddress response.</returns>
        Model.FindAddress.Response FindAddress(Model.FindAddress.Link link);

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">FindAddress request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Request request);

        /// <summary>
        /// Lookup a Postcode or Address as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a FindAddress response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.FindAddress.Response> FindAddressAsync(Model.FindAddress.Link link);

        /// <summary>
        /// Lookup a Postcode. Returns all available data if found.
        /// </summary>
        /// <param name="request">PostcodeLookup request.</param>
        /// <returns>PostcodeLookup response.</returns>
        Model.PostcodeLookup.Response PostcodeLookup(Model.PostcodeLookup.Request request);

        /// <summary>
        /// Lookup a Postcode. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a PostcodeLookup response.</param>
        /// <returns>PostcodeLookup response.</returns>
        Model.PostcodeLookup.Response PostcodeLookup(Model.PostcodeLookup.Link link);

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">PostcodeLookup request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Request request);

        /// <summary>
        /// Lookup a Postcode as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="link">A link returned in a PostcodeLookup response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.PostcodeLookup.Response> PostcodeLookupAsync(Model.PostcodeLookup.Link link);
        
        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied.
        /// </summary>
        /// <param name="request">VerifyAddress request.</param>
        /// <returns>VerifyAddress response.</returns>
        Model.VerifyAddress.Response VerifyAddress(Model.VerifyAddress.Request request);

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>VerifyAddress response.</returns>
        Model.VerifyAddress.Response VerifyAddress(Model.VerifyAddress.Link link);

        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="request">VerifyAddress request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Request request);
        
        /// <summary>
        /// Verify that the address supplied matches the Eircode supplied as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a VerifyAddress response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.VerifyAddress.Response> VerifyAddressAsync(Model.VerifyAddress.Link link);

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id.
        /// </summary>
        /// <param name="request">GetEcadData request.</param>
        /// <returns>GetEcadData response.</returns>
        Model.GetEcadData.Response GetEcadData(Model.GetEcadData.Request request);

        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>GetEcadData response.</returns>
        Model.GetEcadData.Response GetEcadData(Model.GetEcadData.Link link);
        
        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetEcadData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Request request);
        
        /// <summary>
        /// Return all the available data from the ECAD for the supplied ECAD Id as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetEcadData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetEcadData.Response> GetEcadDataAsync(Model.GetEcadData.Link link);

        /// <summary>
        /// Lookup a Postcode or Address auto complete options. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>AutoComplete response.</returns>
        Model.AutoComplete.Response AutoComplete(Model.AutoComplete.Request request);

        /// <summary>
        /// Lookup a Postcode or Address auto complete options as an asynchronous operation. Returns all available data if found.
        /// </summary>
        /// <param name="request">AutoComplete request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.AutoComplete.Response> AutoCompleteAsync(Model.AutoComplete.Request request);

        /// <summary>
        /// Reverse geocode a location. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="request">ReverseGeocode request.</param>
        /// <returns>ReverseGeocode response.</returns>
        Model.ReverseGeocode.Response ReverseGeocode(Model.ReverseGeocode.Request request);

        /// <summary>
        /// Reverse geocode a location. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="link">A link returned in a ReverseGeocode response.</param>
        /// <returns>ReverseGeocode response.</returns>
        Model.ReverseGeocode.Response ReverseGeocode(Model.ReverseGeocode.Link link);

        /// <summary>
        /// Reverse geocode a location as an asynchronous operation. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="request">ReverseGeocode request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.ReverseGeocode.Response> ReverseGeocodeAsync(Model.ReverseGeocode.Request request);

        /// <summary>
        /// Reverse geocode a location as an asynchronous operation. Returns the nearest building to the location within the specified maxDistance.
        /// </summary>
        /// <param name="link">A link returned in a ReverseGeocode response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.ReverseGeocode.Response> ReverseGeocodeAsync(Model.ReverseGeocode.Link link);
        
        /// <summary>
        /// Return data for the supplied UK address ID.
        /// </summary>
        /// <param name="request">GetGbBuildingData request.</param>
        /// <returns>GetGbBuildingData response.</returns>
        Model.GetGbBuildingData.Response GetGbBuildingData(Model.GetGbBuildingData.Request request);

        /// <summary>
        /// Return data for the supplied UK address ID.
        /// </summary>
        /// <param name="link">A link returned in a GetGbBuildingData response.</param>
        /// <returns>GetGbBuildingData response.</returns>
        Model.GetGbBuildingData.Response GetGbBuildingData(Model.GetGbBuildingData.Link link);

        /// <summary>
        /// Return data for the supplied UK address ID as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetGbBuildingData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetGbBuildingData.Response> GetGbBuildingDataAsync(Model.GetGbBuildingData.Request request);

        /// <summary>
        /// Return data for the supplied UK address ID as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetGbBuildingData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetGbBuildingData.Response> GetGbBuildingDataAsync(Model.GetGbBuildingData.Link link);

        /// <summary>
        /// Return data for the supplied UK postcode.
        /// </summary>
        /// <param name="request">GetGbPostcodeData request.</param>
        /// <returns>GetGbPostcodeData response.</returns>
        Model.GetGbPostcodeData.Response GetGbPostcodeData(Model.GetGbPostcodeData.Request request);

        /// <summary>
        /// Return data for the supplied UK postcode.
        /// </summary>
        /// <param name="link">A link returned in a GetGbPostcodeData response.</param>
        /// <returns>GetGbPostcodeData response.</returns>
        Model.GetGbPostcodeData.Response GetGbPostcodeData(Model.GetGbPostcodeData.Link link);

        /// <summary>
        /// Return data for the supplied UK postcode as an asynchronous operation.
        /// </summary>
        /// <param name="request">GetGbPostcodeData request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetGbPostcodeData.Response> GetGbPostcodeDataAsync(Model.GetGbPostcodeData.Request request);

        /// <summary>
        /// Return data for the supplied UK postcode as an asynchronous operation.
        /// </summary>
        /// <param name="link">A link returned in a GetGbPostcodeData response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.GetGbPostcodeData.Response> GetGbPostcodeDataAsync(Model.GetGbPostcodeData.Link link);

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="request">MapId request.</param>
        /// <returns>MapId response.</returns>
        Model.MapId.Response MapId(Model.MapId.Request request);

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="link">A link returned in a MapId response.</param>
        /// <returns>MapId response.</returns>
        Model.MapId.Response MapId(Model.MapId.Link link);

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="request">MapId request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.MapId.Response> MapIdAsync(Model.MapId.Request request);

        /// <summary>
        /// Map ID.
        /// </summary>
        /// <param name="link">A link returned in a MapId response.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Model.MapId.Response> MapIdAsync(Model.MapId.Link link);
    }
}