using System;
using System.Linq;
using System.Threading.Tasks;
using Autoaddress.Autoaddress2_0.Model;
using Xunit;

namespace Autoaddress.Autoaddress2_0.Test.Integration
{
    public class AutoaddressClientTest
    {
        [Fact]
        public void FindAddress_IE_8SilverBirchesDunboyneCoDotMeath_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne, Co. Meath";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);
            
            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public void FindAddress_IE_8SilverBirchesDunboyneCoDotMeathA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne, Co. Meath, A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeValidated, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public void FindAddress_IE_8SilverBirchesDunboyneCoDotMeathA86VC05_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne, Co. Meath, A86VC05";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAmended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public void FindAddress_IE_9SilverBirchesDunboyneCoDotMeathA86VC04_ReturnsValidResponse()
        {
            const string address = "9 Silver Birches, Dunboyne, Co. Meath, A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.AddressAmendedToMatchPostcode, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public void FindAddress_IE_8SilverBirchesDunboyneInvalidLicenceKey_ThrowsAutoaddressException()
        {
            const string licenceKey = "InvalidLicenceKey";
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(licenceKey);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            AutoaddressException autoaddressException = Assert.Throws<AutoaddressException>(() => autoaddressClient.FindAddress(request));
            Assert.Equal(ErrorType.InvalidLicenceKey, autoaddressException.ErrorType);
        }

        [Fact]
        public void FindAddress_IE_8SilverBirchesDunboyneUseKeyFromAppConfig_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public void FindAddress_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.True(response.IsUniqueAddress.HasValue && !response.IsUniqueAddress.Value);
            Assert.NotNull(response.Options);
        }

        [Fact]
        public void FindAddress_IE_SilverBirchesDunboyneThenSelectFirstOption_ReturnsValidResponses()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.FindAddress(request);

            Assert.NotNull(firstResponse);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.IncompleteAddressEntered, firstResponse.Result);
            Assert.True(firstResponse.IsUniqueAddress.HasValue && !firstResponse.IsUniqueAddress.Value);
            Assert.NotNull(firstResponse.Options);
            var option = firstResponse.Options[0];
            var nextLink = option.Links.OfType<Model.FindAddress.Link>().First();

            var secondResponse = autoaddressClient.FindAddress(nextLink);
            Assert.NotNull(secondResponse);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.IncompleteAddressEntered, secondResponse.Result);
            Assert.True(secondResponse.IsUniqueAddress.HasValue && !secondResponse.IsUniqueAddress.Value);
            Assert.NotNull(secondResponse.Options);
        }

        [Fact]
        public void FindAddress_IE_SilverBirchesDunboyneThenSelectSelfLink_ReturnsValidResponses()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.FindAddress(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.True(firstResponse.Links.Length > 0);
            var link = firstResponse.Links.OfType<Model.FindAddress.Link>().First();

            var secondResponse = autoaddressClient.FindAddress(link);

            Assert.NotNull(secondResponse);
            Assert.Equal(firstResponse.Result, secondResponse.Result);
            Assert.Equal(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Fact]
        public void FindAddress_IE_1WoodlandsRoadCabinteelyDublin18_vanityModeEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string address = "1 Woodlands Rd, Cabinteely, Dublin 18";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: true, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A96E2R8", response.Postcode);
            Assert.Null(response.Unmatched);
            Assert.Null(response.UnmatchedAddressElements);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal("1 WOODLANDS ROAD", response.PostalAddress[0]);
            Assert.Equal("GLENAGEARY", response.PostalAddress[1]);
            Assert.Equal("CO. DUBLIN", response.PostalAddress[2]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.Equal(4, response.PostalAddressElements.Length);
            Assert.Equal("1", response.PostalAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingNumber, response.PostalAddressElements[0].Type);
            Assert.Equal(1401042441, response.PostalAddressElements[0].AddressId);
            Assert.Equal("WOODLANDS ROAD", response.PostalAddressElements[1].Value);
            Assert.Equal(AddressElementType.Thoroughfare, response.PostalAddressElements[1].Type);
            Assert.Equal(1200029775, response.PostalAddressElements[1].AddressId);
            Assert.Equal("GLENAGEARY", response.PostalAddressElements[2].Value);
            Assert.Equal(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.Equal(1100000090, response.PostalAddressElements[2].AddressId);
            Assert.Equal("CO. DUBLIN", response.PostalAddressElements[3].Value);
            Assert.Equal(AddressElementType.County, response.PostalAddressElements[3].Type);
            Assert.Equal(1001000025, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.VanityAddress);
            Assert.Equal("1 Woodlands Road", response.VanityAddress[0]);
            Assert.Equal("Cabinteely", response.VanityAddress[1]);
            Assert.Equal("Dublin 18", response.VanityAddress[2]);
            Assert.NotNull(response.VanityAddressElements);
            Assert.Equal(4, response.VanityAddressElements.Length);
            Assert.Equal("1", response.VanityAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingNumber, response.VanityAddressElements[0].Type);
            Assert.Equal(1401042441, response.VanityAddressElements[0].AddressId);
            Assert.Equal("Woodlands Road", response.VanityAddressElements[1].Value);
            Assert.Equal(AddressElementType.Thoroughfare, response.VanityAddressElements[1].Type);
            Assert.Equal(1200029775, response.VanityAddressElements[1].AddressId);
            Assert.Equal("Cabinteely", response.VanityAddressElements[2].Value);
            Assert.Equal(AddressElementType.UrbanArea, response.VanityAddressElements[2].Type);
            Assert.Equal(1110029573, response.VanityAddressElements[2].AddressId);
            Assert.Equal("Dublin 18", response.VanityAddressElements[3].Value);
            Assert.Equal(AddressElementType.DublinPostalArea, response.VanityAddressElements[3].Type);
            Assert.Equal(1100000017, response.VanityAddressElements[3].AddressId);
            Assert.NotNull(response.CleanResult);
        }

        [Fact]
        public void FindAddress_IE_TerminalBuildingShannonAirportShannonCoDotClare_geographicAddressEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string address = "Terminal Building, Shannon Airport, Shannon, Co. Clare";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: true, vanityMode: false, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("V14NX04", response.Postcode);
            Assert.Null(response.Unmatched);
            Assert.Null(response.UnmatchedAddressElements);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal("TERMINAL BUILDING", response.PostalAddress[0]);
            Assert.Equal("SHANNON AIRPORT", response.PostalAddress[1]);
            Assert.Equal("SHANNON", response.PostalAddress[2]);
            Assert.Equal("LIMERICK", response.PostalAddress[3]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.Equal(4, response.PostalAddressElements.Length);
            Assert.Equal("TERMINAL BUILDING", response.PostalAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingName, response.PostalAddressElements[0].Type);
            Assert.Equal(1401207129, response.PostalAddressElements[0].AddressId);
            Assert.Equal("SHANNON AIRPORT", response.PostalAddressElements[1].Value);
            Assert.Equal(AddressElementType.RuralLocality, response.PostalAddressElements[1].Type);
            Assert.Equal(1110026207, response.PostalAddressElements[1].AddressId);
            Assert.Equal("SHANNON", response.PostalAddressElements[2].Value);
            Assert.Equal(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.Equal(1100000099, response.PostalAddressElements[2].AddressId);
            Assert.Equal("LIMERICK", response.PostalAddressElements[3].Value);
            Assert.Equal(AddressElementType.City, response.PostalAddressElements[3].Type);
            Assert.Equal(1100000030, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.GeographicAddress);
            Assert.Equal("TERMINAL BUILDING", response.GeographicAddress[0]);
            Assert.Equal("SHANNON AIRPORT", response.GeographicAddress[1]);
            Assert.Equal("SHANNON", response.GeographicAddress[2]);
            Assert.Equal("CO. CLARE", response.GeographicAddress[3]);
            Assert.NotNull(response.GeographicAddressElements);
            Assert.Equal(4, response.GeographicAddressElements.Length);
            Assert.Equal("TERMINAL BUILDING", response.GeographicAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingName, response.GeographicAddressElements[0].Type);
            Assert.Equal(1401207129, response.GeographicAddressElements[0].AddressId);
            Assert.Equal("SHANNON AIRPORT", response.GeographicAddressElements[1].Value);
            Assert.Equal(AddressElementType.RuralLocality, response.GeographicAddressElements[1].Type);
            Assert.Equal(1110026207, response.GeographicAddressElements[1].AddressId);
            Assert.Equal("SHANNON", response.GeographicAddressElements[2].Value);
            Assert.Equal(AddressElementType.Town, response.GeographicAddressElements[2].Type);
            Assert.Equal(1100000099, response.GeographicAddressElements[2].AddressId);
            Assert.Equal("CO. CLARE", response.GeographicAddressElements[3].Value);
            Assert.Equal(AddressElementType.County, response.GeographicAddressElements[3].Type);
            Assert.Equal(1001000003, response.GeographicAddressElements[3].AddressId);
            Assert.Null(response.VanityAddress);
            Assert.NotNull(response.CleanResult);
        }

        [Fact]
        public async Task FindAddressAsync_IE_8SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.True(response.IsUniqueAddress.HasValue && response.IsUniqueAddress.Value);
            Assert.Equal("A86VC04", response.Postcode);
        }

        [Fact]
        public async Task FindAddressAsync_IE_8SilverBirchesDunboyneInvalidLicenceKey_ThrowsAutoaddressException()
        {
            const string licenceKey = "InvalidLicenceKey";
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(licenceKey);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            await Assert.ThrowsAsync<AutoaddressException>(async () => await autoaddressClient.FindAddressAsync(request));
        }

        [Fact]
        public async Task FindAddressAsync_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
        }

        [Fact]
        public void PostcodeLookup_IE_A86VC04_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(4, response.PostalAddress.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.Equal("MILLFARM", response.PostalAddress[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress[3]);
        }

        [Fact]
        public void PostcodeLookup_IE_A96E2R8_vanityModeEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string postcode = "A96E2R8";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: true, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.Equal("A96E2R8", response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal("1 WOODLANDS ROAD", response.PostalAddress[0]);
            Assert.Equal("GLENAGEARY", response.PostalAddress[1]);
            Assert.Equal("CO. DUBLIN", response.PostalAddress[2]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.Equal(4, response.PostalAddressElements.Length);
            Assert.Equal("1", response.PostalAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingNumber, response.PostalAddressElements[0].Type);
            Assert.Equal(1401042441, response.PostalAddressElements[0].AddressId);
            Assert.Equal("WOODLANDS ROAD", response.PostalAddressElements[1].Value);
            Assert.Equal(AddressElementType.Thoroughfare, response.PostalAddressElements[1].Type);
            Assert.Equal(1200029775, response.PostalAddressElements[1].AddressId);
            Assert.Equal("GLENAGEARY", response.PostalAddressElements[2].Value);
            Assert.Equal(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.Equal(1100000090, response.PostalAddressElements[2].AddressId);
            Assert.Equal("CO. DUBLIN", response.PostalAddressElements[3].Value);
            Assert.Equal(AddressElementType.County, response.PostalAddressElements[3].Type);
            Assert.Equal(1001000025, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.VanityAddress);
            Assert.Equal("1 Woodlands Road", response.VanityAddress[0]);
            Assert.Equal("Dun Laoghaire", response.VanityAddress[1]);
            Assert.Equal("Co. Dublin", response.VanityAddress[2]);
            Assert.NotNull(response.VanityAddressElements);
            Assert.Equal(4, response.VanityAddressElements.Length);
            Assert.Equal("1", response.VanityAddressElements[0].Value);
            Assert.Equal(AddressElementType.BuildingNumber, response.VanityAddressElements[0].Type);
            Assert.Equal(1401042441, response.VanityAddressElements[0].AddressId);
            Assert.Equal("Woodlands Road", response.VanityAddressElements[1].Value);
            Assert.Equal(AddressElementType.Thoroughfare, response.VanityAddressElements[1].Type);
            Assert.Equal(1200029775, response.VanityAddressElements[1].AddressId);
            Assert.Equal("Dun Laoghaire", response.VanityAddressElements[2].Value);
            Assert.Equal(AddressElementType.Locality, response.VanityAddressElements[2].Type);
            Assert.Equal(1100000131, response.VanityAddressElements[2].AddressId);
            Assert.Equal("Co. Dublin", response.VanityAddressElements[3].Value);
            Assert.Equal(AddressElementType.County, response.VanityAddressElements[3].Type);
            Assert.Equal(1001000025, response.VanityAddressElements[3].AddressId);
        }

        [Fact]
        public void PostcodeLookup_IE_A86VC04ThenSelectSelfLink_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.PostcodeLookup(request);
            
            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.True(firstResponse.Links.Length > 0);
            var link = firstResponse.Links.OfType<Model.PostcodeLookup.Link>().First();

            var secondResponse = autoaddressClient.PostcodeLookup(link);

            Assert.NotNull(secondResponse);
            Assert.Equal(firstResponse.Result, secondResponse.Result);
            Assert.Equal(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Fact]
        public void PostcodeLookup_IE_D08XY00_ReturnsValidResponse()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(3, response.PostalAddress.Length);
            Assert.Equal("4 INNS COURT", response.PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", response.PostalAddress[1]);
            Assert.Equal("DUBLIN 8", response.PostalAddress[2]);
            Assert.NotNull(response.Options);
            Assert.Equal(3, response.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Fact]
        public void PostcodeLookup_IE_D08XY00ThenSelectGammaFromOptions_ReturnsValidResponses()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(firstResponse);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.Equal(postcode, firstResponse.Postcode);
            Assert.NotNull(firstResponse.PostalAddress);
            Assert.Equal(3, firstResponse.PostalAddress.Length);
            Assert.Equal("4 INNS COURT", firstResponse.PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", firstResponse.PostalAddress[1]);
            Assert.Equal("DUBLIN 8", firstResponse.PostalAddress[2]);
            Assert.NotNull(firstResponse.Options);
            Assert.Equal(3, firstResponse.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[2].DisplayName);
            Assert.NotNull(firstResponse.Options[2].Links);
            Assert.True(firstResponse.Options[2].Links.Length > 0);
            Assert.NotNull(firstResponse.Options[2].Links[0]);

            var secondResponse = autoaddressClient.PostcodeLookup(firstResponse.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.Equal(postcode, firstResponse.Postcode);
            Assert.Equal(AddressType.Organisation, secondResponse.AddressType);
        }

        [Fact]
        public void PostcodeLookup_IE_F94H289_ReturnsValidResponse()
        {
            const string postcode = "F94H289";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.RetiredPostcode, response.Result);
            Assert.Equal("F94H289", response.Postcode);
            Assert.Equal(1700276437, response.AddressId);
            Assert.Equal(MatchLevel.Unknown, response.MatchLevel);
            Assert.Null(response.AddressType);
            Assert.Null(response.PostalAddress);
            Assert.Null(response.PostalAddressElements);
            Assert.Null(response.VanityAddress);
            Assert.Null(response.VanityAddressElements);
            Assert.Null(response.ReformattedAddressResult);
            Assert.Null(response.ReformattedAddress);
            Assert.Equal(0, response.TotalOptions);
            Assert.NotNull(response.Options);
            Assert.Equal(0, response.Options.Length);
        }

        [Fact]
        public void PostcodeLookup_IE_Y35F9KX_ReturnsValidResponse()
        {
            const string postcode = "Y35F9KX";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ChangedPostcode, response.Result);
            Assert.NotNull(response.Postcode);
            Assert.Equal("Y35TD51", response.Postcode);
            Assert.Equal(1702151625, response.AddressId);
            Assert.Equal(MatchLevel.AddressPoint, response.MatchLevel);
            Assert.Equal(AddressType.ResidentialAddressPoint, response.AddressType);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(3, response.PostalAddress.Length);
            Assert.Equal("22 HILLCREST", response.PostalAddress[0]);
            Assert.Equal("MULGANNON", response.PostalAddress[1]);
            Assert.Equal("WEXFORD", response.PostalAddress[2]);
            Assert.Null(response.PostalAddressElements);
            Assert.Null(response.VanityAddress);
            Assert.Null(response.VanityAddressElements);
            Assert.Equal(0, response.TotalOptions);
            Assert.NotNull(response.Options);
            Assert.Equal(0, response.Options.Length);
        }

        [Fact]
        public void FindAddress_NI_9AvocaCloseBelfast_ReturnsValidResponse()
        {
            const string address = "9 Avoca Close, Belfast";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.NI, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.False(response.IsUniqueAddress.HasValue);
            Assert.Equal("BT11 8QT", response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(2, response.PostalAddress.Length);
            Assert.Equal("9 AVOCA CLOSE", response.PostalAddress[0]);
            Assert.Equal("BELFAST", response.PostalAddress[1]);
        }

        [Fact]
        public async Task PostcodeLookupAsync_IE_A86VC04_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(4, response.PostalAddress.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.Equal("MILLFARM", response.PostalAddress[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress[3]);
        }

        [Fact]
        public async Task PostcodeLookupAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(3, response.PostalAddress.Length);
            Assert.Equal("4 INNS COURT", response.PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", response.PostalAddress[1]);
            Assert.Equal("DUBLIN 8", response.PostalAddress[2]);
            Assert.NotNull(response.Options);
            Assert.Equal(3, response.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Fact]
        public async Task PostcodeLookupAsync_IE_D08XY00ThenSelectGammaFromOptions_ReturnsValidResponses()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(firstResponse);
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.Equal(postcode, firstResponse.Postcode);
            Assert.NotNull(firstResponse.PostalAddress);
            Assert.Equal(3, firstResponse.PostalAddress.Length);
            Assert.Equal("4 INNS COURT", firstResponse.PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", firstResponse.PostalAddress[1]);
            Assert.Equal("DUBLIN 8", firstResponse.PostalAddress[2]);
            Assert.NotNull(firstResponse.Options);
            Assert.Equal(3, firstResponse.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[2].DisplayName);
            Assert.NotNull(firstResponse.Options[2].Links);
            Assert.True(firstResponse.Options[2].Links.Length > 0);
            Assert.NotNull(firstResponse.Options[2].Links[0]);

            var secondResponse = await autoaddressClient.PostcodeLookupAsync(firstResponse.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            Assert.Equal(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.Equal(postcode, firstResponse.Postcode);
            Assert.Equal(AddressType.Organisation, secondResponse.AddressType);
        }

        [Fact]
        public void VerifyAddress_IE_8SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var response = autoaddressClient.VerifyAddress(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.VerifyAddress.ReturnCode.AddressAndEircodeMatch, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(4, response.PostalAddress.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.Equal("MILLFARM", response.PostalAddress[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress[3]);
        }

        [Fact]
        public void VerifyAddress_IE_8SilverBirchesDunboyneA86VC04ThenSelectSelfLink_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var firstResponse = autoaddressClient.VerifyAddress(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.True(firstResponse.Links.Length > 0);
            var link = firstResponse.Links.OfType<Model.VerifyAddress.Link>().First();

            var secondResponse = autoaddressClient.VerifyAddress(link);

            Assert.NotNull(secondResponse);
            Assert.Equal(firstResponse.Result, secondResponse.Result);
            Assert.Equal(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Fact]
        public async Task VerifyAddressAsync_IE_8SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var response = await autoaddressClient.VerifyAddressAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.VerifyAddress.ReturnCode.AddressAndEircodeMatch, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.Equal(4, response.PostalAddress.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.Equal("MILLFARM", response.PostalAddress[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress[3]);
        }

        [Fact]
        public void GetEcadData_1701984269_ReturnsValidResponse()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal(ecadId, response.EcadId);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Current, response.EcadIdStatus);
            Assert.Equal(2150, response.AddressTypeId);
            Assert.NotNull(response.EircodeInfo);
            Assert.Equal(ecadId, response.EircodeInfo.EcadId);
            Assert.Equal("A86VC04", response.EircodeInfo.Eircode);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.Equal(4, response.PostalAddress.English.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress.English[0]);
            Assert.Equal("MILLFARM", response.PostalAddress.English[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress.English[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress.English[3]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.Equal(4, response.PostalAddress.Irish.Length);
            Assert.Equal("8 NA BEITHEANNA GEALA", response.PostalAddress.Irish[0]);
            Assert.Equal("FEIRM AN MHUILINN", response.PostalAddress.Irish[1]);
            Assert.Equal("DÚN BÚINNE", response.PostalAddress.Irish[2]);
            Assert.Equal("CO. NA MÍ", response.PostalAddress.Irish[3]);
            Assert.NotNull(response.GeographicAddress);
            Assert.NotNull(response.GeographicAddress.English);
            Assert.Equal(4, response.GeographicAddress.English.Length);
            Assert.Equal("8 SILVER BIRCHES", response.GeographicAddress.English[0]);
            Assert.Equal("MILLFARM", response.GeographicAddress.English[1]);
            Assert.Equal("DUNBOYNE", response.GeographicAddress.English[2]);
            Assert.Equal("CO. MEATH", response.GeographicAddress.English[3]);
            Assert.NotNull(response.GeographicAddress.Irish);
            Assert.Equal(4, response.GeographicAddress.Irish.Length);
            Assert.Equal("8 NA BEITHEANNA GEALA", response.GeographicAddress.Irish[0]);
            Assert.Equal("FEIRM AN MHUILINN", response.GeographicAddress.Irish[1]);
            Assert.Equal("DÚN BÚINNE", response.GeographicAddress.Irish[2]);
            Assert.Equal("CO. NA MÍ", response.GeographicAddress.Irish[3]);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.Equal(1400247786, response.AdministrativeInfo.EcadId);
            Assert.Equal("2015", response.AdministrativeInfo.Release);
            Assert.Equal(16, response.AdministrativeInfo.LaId);
            Assert.Equal(167029, response.AdministrativeInfo.DedId);
            Assert.Equal(14588, response.AdministrativeInfo.SmallAreaId);
            Assert.Equal(160648, response.AdministrativeInfo.TownlandId);
            Assert.Equal(1001000020, response.AdministrativeInfo.CountyId);
            Assert.Equal(false, response.AdministrativeInfo.Gaeltacht);
            Assert.NotNull(response.BuildingInfo);
            Assert.Equal(1400247786, response.BuildingInfo.EcadId);
            Assert.Equal(1, response.BuildingInfo.BuildingTypeId);
            Assert.Equal(false, response.BuildingInfo.UnderConstruction);
            Assert.Equal("R", response.BuildingInfo.BuildingUse);
            Assert.Equal(false, response.BuildingInfo.Vacant);
            Assert.Equal(null, response.BuildingInfo.HolidayHome);
            Assert.Null(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.True(response.SpatialInfo.Etrs89.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.Location.Longitude < 0);
            Assert.Null(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.Location);
            Assert.True(response.SpatialInfo.Ing.Location.Easting > 0);
            Assert.True(response.SpatialInfo.Ing.Location.Northing > 0);
            Assert.Null(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.Location);
            Assert.True(response.SpatialInfo.Itm.Location.Easting > 0);
            Assert.True(response.SpatialInfo.Itm.Location.Northing > 0);
            Assert.Null(response.SpatialInfo.Itm.BoundingBox);
            Assert.Equal("3", response.SpatialInfo.SpatialAccuracy);
            Assert.NotNull(response.RelatedEcadIds);
            Assert.Null(response.RelatedEcadIds.AddressPointEcadId);
            Assert.Equal(1400247786, response.RelatedEcadIds.BuildingEcadId);
            Assert.Null(response.RelatedEcadIds.BuildingGroupEcadId);
            Assert.NotNull(response.RelatedEcadIds.ThoroughfareEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.ThoroughfareEcadIds.Length);
            Assert.Equal(1200021757, response.RelatedEcadIds.ThoroughfareEcadIds[0]);
            Assert.NotNull(response.RelatedEcadIds.LocalityEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.LocalityEcadIds.Length);
            Assert.Equal(1110026424, response.RelatedEcadIds.LocalityEcadIds[0]);
            Assert.NotNull(response.RelatedEcadIds.PostTownEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.PostTownEcadIds.Length);
            Assert.Equal(1100000117, response.RelatedEcadIds.PostTownEcadIds[0]);
            Assert.NotNull(response.RelatedEcadIds.PostCountyEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.PostCountyEcadIds.Length);
            Assert.Equal(1001000020, response.RelatedEcadIds.PostCountyEcadIds[0]);
            Assert.NotNull(response.RelatedEcadIds.GeographicCountyEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.GeographicCountyEcadIds.Length);
            Assert.Equal(1001000020, response.RelatedEcadIds.GeographicCountyEcadIds[0]);
            Assert.NotNull(response.DateInfo);
            Assert.NotNull(response.DateInfo.Created);
            Assert.NotNull(response.DateInfo.Modified);
            Assert.True(response.DateInfo.Created.Value.Kind == DateTimeKind.Utc);
            Assert.True(response.DateInfo.Modified.Value.Kind == DateTimeKind.Utc);
            Assert.True(new DateTime(2014, 07, 04, 15, 50, 00, DateTimeKind.Utc) < response.DateInfo.Created.Value);
            Assert.True(response.DateInfo.Created.Value < new DateTime(2014, 07, 04, 16, 00, 00, DateTimeKind.Utc));
            Assert.True(new DateTime(2017, 02, 03, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Modified.Value);
        }

        [Fact]
        public void GetEcadData_1701984269ThenSelectSelfLink_ReturnsValidResponses()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var firstResponse = autoaddressClient.GetEcadData(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.True(firstResponse.Links.Length > 0);
            var link = firstResponse.Links.OfType<Model.GetEcadData.Link>().First();

            var secondResponse = autoaddressClient.GetEcadData(link);

            Assert.NotNull(secondResponse);
            Assert.Equal(firstResponse.Result, secondResponse.Result);
            Assert.Equal(firstResponse.EcadId, secondResponse.EcadId);
        }

        [Fact]
        public void GetEcadData_1200003223_ReturnsValidResponse()
        {
            const int ecadId = 1200003223;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal(ecadId, response.EcadId);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Current, response.EcadIdStatus);
            Assert.Equal(3100, response.AddressTypeId);
            Assert.Null(response.EircodeInfo);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.Equal(2, response.PostalAddress.English.Length);
            Assert.Equal("DAME STREET", response.PostalAddress.English[0]);
            Assert.Equal("DUBLIN 2", response.PostalAddress.English[1]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.Equal(2, response.PostalAddress.Irish.Length);
            Assert.Equal("SRÁID AN DÁMA", response.PostalAddress.Irish[0]);
            Assert.Equal("BAILE ÁTHA CLIATH 2", response.PostalAddress.Irish[1]);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.Equal(ecadId, response.AdministrativeInfo.EcadId);
            Assert.Equal("2015", response.AdministrativeInfo.Release);
            Assert.Equal(268140, response.AdministrativeInfo.DedId);
            Assert.Equal(false, response.AdministrativeInfo.Gaeltacht);
            Assert.Equal(29, response.AdministrativeInfo.LaId);
            Assert.Null(response.AdministrativeInfo.SmallAreaId);
            Assert.Null(response.AdministrativeInfo.TownlandId);
            Assert.Equal(1001000025, response.AdministrativeInfo.CountyId);
            Assert.Null(response.BuildingInfo);
            Assert.Null(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.Equal(ecadId, response.SpatialInfo.EcadId);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.True(response.SpatialInfo.Etrs89.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.Location.Longitude < 0);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox.Max);
            Assert.True(response.SpatialInfo.Etrs89.BoundingBox.Min.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.BoundingBox.Min.Longitude < 0);
            Assert.True(response.SpatialInfo.Etrs89.BoundingBox.Max.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.BoundingBox.Max.Longitude < 0);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.True(response.SpatialInfo.Etrs89.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.Location.Longitude < 0);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox.Max);
            Assert.True(response.SpatialInfo.Ing.BoundingBox.Min.Easting > 0);
            Assert.True(response.SpatialInfo.Ing.BoundingBox.Min.Northing > 0);
            Assert.True(response.SpatialInfo.Ing.BoundingBox.Max.Easting > 0);
            Assert.True(response.SpatialInfo.Ing.BoundingBox.Max.Northing > 0);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox.Max);
            Assert.True(response.SpatialInfo.Itm.BoundingBox.Min.Easting > 0);
            Assert.True(response.SpatialInfo.Itm.BoundingBox.Min.Northing > 0);
            Assert.True(response.SpatialInfo.Itm.BoundingBox.Max.Easting > 0);
            Assert.True(response.SpatialInfo.Itm.BoundingBox.Max.Northing > 0);
            Assert.Equal("3", response.SpatialInfo.SpatialAccuracy);
            Assert.NotNull(response.RelatedEcadIds);
        }

        [Fact]
        public async Task GetEcadDataAsync_1701984269_ReturnsValidResponse()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var response = await autoaddressClient.GetEcadDataAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal(ecadId, response.EcadId);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Current, response.EcadIdStatus);
            Assert.Equal(2150, response.AddressTypeId);
            Assert.NotNull(response.EircodeInfo);
            Assert.Equal(ecadId, response.EircodeInfo.EcadId);
            Assert.Equal("A86VC04", response.EircodeInfo.Eircode);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.Equal(4, response.PostalAddress.English.Length);
            Assert.Equal("8 SILVER BIRCHES", response.PostalAddress.English[0]);
            Assert.Equal("MILLFARM", response.PostalAddress.English[1]);
            Assert.Equal("DUNBOYNE", response.PostalAddress.English[2]);
            Assert.Equal("CO. MEATH", response.PostalAddress.English[3]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.Equal(4, response.PostalAddress.Irish.Length);
            Assert.Equal("8 NA BEITHEANNA GEALA", response.PostalAddress.Irish[0]);
            Assert.Equal("FEIRM AN MHUILINN", response.PostalAddress.Irish[1]);
            Assert.Equal("DÚN BÚINNE", response.PostalAddress.Irish[2]);
            Assert.Equal("CO. NA MÍ", response.PostalAddress.Irish[3]);
            Assert.Null(response.Input.AdministrativeInfo);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.Equal(1400247786, response.AdministrativeInfo.EcadId);
            Assert.Equal("2015", response.AdministrativeInfo.Release);
            Assert.Equal(16, response.AdministrativeInfo.LaId);
            Assert.Equal(167029, response.AdministrativeInfo.DedId);
            Assert.Equal(14588, response.AdministrativeInfo.SmallAreaId);
            Assert.Equal(160648, response.AdministrativeInfo.TownlandId);
            Assert.Equal(1001000020, response.AdministrativeInfo.CountyId);
            Assert.Equal(false, response.AdministrativeInfo.Gaeltacht);
            Assert.NotNull(response.BuildingInfo);
            Assert.Equal(1400247786, response.BuildingInfo.EcadId);
            Assert.Equal(1, response.BuildingInfo.BuildingTypeId);
            Assert.Equal(false, response.BuildingInfo.UnderConstruction);
            Assert.Equal("R", response.BuildingInfo.BuildingUse);
            Assert.Equal(false, response.BuildingInfo.Vacant);
            Assert.Equal(null, response.BuildingInfo.HolidayHome);
            Assert.Null(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.True(response.SpatialInfo.Etrs89.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Etrs89.Location.Longitude < 0);
            Assert.Null(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.Location);
            Assert.True(response.SpatialInfo.Ing.Location.Easting > 0);
            Assert.True(response.SpatialInfo.Ing.Location.Northing > 0);
            Assert.Null(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.Location);
            Assert.True(response.SpatialInfo.Itm.Location.Easting > 0);
            Assert.True(response.SpatialInfo.Itm.Location.Northing > 0);
            Assert.Null(response.SpatialInfo.Itm.BoundingBox);
            Assert.Equal("3", response.SpatialInfo.SpatialAccuracy);
            Assert.NotNull(response.RelatedEcadIds);
            Assert.NotNull(response.RelatedEcadIds.GeographicCountyEcadIds);
            Assert.Equal(1, response.RelatedEcadIds.GeographicCountyEcadIds.Length);
            Assert.Equal(1001000020, response.RelatedEcadIds.GeographicCountyEcadIds[0]);
            Assert.NotNull(response.DateInfo);
            Assert.NotNull(response.DateInfo.Created);
            Assert.NotNull(response.DateInfo.Modified);
            Assert.True(response.DateInfo.Created.Value.Kind == DateTimeKind.Utc);
            Assert.True(response.DateInfo.Modified.Value.Kind == DateTimeKind.Utc);
            Assert.True(new DateTime(2014, 07, 04, 15, 50, 00, DateTimeKind.Utc) < response.DateInfo.Created.Value);
            Assert.True(response.DateInfo.Created.Value < new DateTime(2014, 07, 04, 16, 00, 00, DateTimeKind.Utc));
            Assert.True(new DateTime(2017, 02, 03, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Modified.Value);
        }

        [Fact]
        public void GetEcadData_9999999999_ReturnsInvalidResponse()
        {
            const int ecadId = 999999999;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdInvalid, response.Result);
            Assert.Null(response.DateInfo);
        }

        [Fact]
        public void GetEcadData_1110000147_HistoryEqualsFalse_ReturnsEcadIdValidAndChanged()
        {
            const int ecadId = 1110000147;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Changed, response.EcadIdStatus);
            Assert.NotNull(response.DateInfo);
            Assert.NotNull(response.DateInfo.Created);
            Assert.NotNull(response.DateInfo.Modified);
            Assert.True(response.DateInfo.Created.Value.Kind == DateTimeKind.Utc);
            Assert.True(response.DateInfo.Modified.Value.Kind == DateTimeKind.Utc);
            Assert.True(new DateTime(2014, 10, 08, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Created.Value);
            Assert.True(response.DateInfo.Created.Value < new DateTime(2014, 10, 08, 12, 00, 00, DateTimeKind.Utc));
            Assert.True(new DateTime(2014, 10, 08, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Modified.Value);
            Assert.True(response.DateInfo.Modified.Value < new DateTime(2014, 10, 08, 12, 00, 00, DateTimeKind.Utc));
        }

        [Fact]
        public void GetEcadData_1110000147_HistoryEqualsTrue_ReturnsEcadIdInvalidAndRetired()
        {
            const int ecadId = 1110000147;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, true);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdInvalid, response.Result);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Retired, response.EcadIdStatus);
            Assert.NotNull(response.DateInfo);
            Assert.NotNull(response.DateInfo.Created);
            Assert.NotNull(response.DateInfo.Modified);
            Assert.True(response.DateInfo.Created.Value.Kind == DateTimeKind.Utc);
            Assert.True(response.DateInfo.Modified.Value.Kind == DateTimeKind.Utc);
            Assert.True(new DateTime(2017, 02, 02, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Created.Value);
            Assert.True(response.DateInfo.Created.Value < new DateTime(2017, 05, 02, 23, 59, 59, DateTimeKind.Utc));
            Assert.True(new DateTime(2017, 02, 02, 00, 00, 00, DateTimeKind.Utc) < response.DateInfo.Modified.Value);
            Assert.True(response.DateInfo.Modified.Value < new DateTime(2017, 05, 02, 23, 59, 59, DateTimeKind.Utc));
        }

        [Fact]
        public void AutoComplete_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.AutoComplete(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.Equal(1, response.Options.Length);
            Assert.Equal("SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH", response.Options[0].DisplayName);
        }

        [Fact]
        public async Task AutoCompleteAsync_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.Equal(1, response.Options.Length);
            Assert.Equal("SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH", response.Options[0].DisplayName);
        }

        [Fact]
        public void AutoComplete_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.AutoComplete(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.Equal(3, response.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Fact]
        public async Task AutoCompleteAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.Equal(3, response.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Fact]
        public void AutoCompleteThenFindAddress_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var autoCompleteResponse = autoaddressClient.AutoComplete(request);

            Assert.NotNull(autoCompleteResponse);
            Assert.NotNull(autoCompleteResponse.Options);
            Assert.Equal(3, autoCompleteResponse.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[2].DisplayName);

            var link = autoCompleteResponse.Options[1].Links.OfType<Model.FindAddress.Link>().First();

            var findAddressResponse = autoaddressClient.FindAddress(link);
            
            Assert.NotNull(findAddressResponse);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, findAddressResponse.Result);
            Assert.Equal(eircode, findAddressResponse.Postcode);
            Assert.NotNull(findAddressResponse.PostalAddress);
            Assert.Equal(4, findAddressResponse.PostalAddress.Length);
            Assert.Equal("AUTOADDRESS", findAddressResponse.PostalAddress[0]);
            Assert.Equal("4 INNS COURT", findAddressResponse.PostalAddress[1]);
            Assert.Equal("WINETAVERN STREET", findAddressResponse.PostalAddress[2]);
            Assert.Equal("DUBLIN 8", findAddressResponse.PostalAddress[3]);
        }

        [Fact]
        public async Task AutoCompleteAsyncThenFindAddressAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var autoCompleteResponse = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(autoCompleteResponse);
            Assert.NotNull(autoCompleteResponse.Options);
            Assert.Equal(3, autoCompleteResponse.Options.Length);
            Assert.Equal("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[0].DisplayName);
            Assert.Equal("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[1].DisplayName);
            Assert.Equal("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[2].DisplayName);

            var link = autoCompleteResponse.Options[1].Links.OfType<Model.FindAddress.Link>().First();

            var findAddressResponse = await autoaddressClient.FindAddressAsync(link);
            
            Assert.NotNull(findAddressResponse);
            Assert.Equal(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, findAddressResponse.Result);
            Assert.Equal(eircode, findAddressResponse.Postcode);
            Assert.NotNull(findAddressResponse.PostalAddress);
            Assert.Equal(4, findAddressResponse.PostalAddress.Length);
            Assert.Equal("AUTOADDRESS", findAddressResponse.PostalAddress[0]);
            Assert.Equal("4 INNS COURT", findAddressResponse.PostalAddress[1]);
            Assert.Equal("WINETAVERN STREET", findAddressResponse.PostalAddress[2]);
            Assert.Equal("DUBLIN 8", findAddressResponse.PostalAddress[3]);
        }

        [Fact]
        public void ReverseGeocode_LongitudeEqualsMinus6Point271796_LatitudeEquals53Point343761_ReturnsValidResponse()
        {
            const double longitude = -6.271796;
            const double latitude = 53.343761;
            const double maxDistance = 100;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.ReverseGeocode.Request(latitude: latitude, longitude: longitude, maxDistance: maxDistance, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false, addressProfileName: null);

            var reverseGeocodeResponse = autoaddressClient.ReverseGeocode(request);

            Assert.NotNull(reverseGeocodeResponse);
            Assert.NotNull(reverseGeocodeResponse.Hits);
            Assert.Equal(1, reverseGeocodeResponse.Hits.Length);
            Assert.Equal(1401182204, reverseGeocodeResponse.Hits[0].AddressId);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].PostalAddress);
            Assert.Equal(3, reverseGeocodeResponse.Hits[0].PostalAddress.Length);
            Assert.Equal("INNS COURT", reverseGeocodeResponse.Hits[0].PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", reverseGeocodeResponse.Hits[0].PostalAddress[1]);
            Assert.Equal("DUBLIN 8", reverseGeocodeResponse.Hits[0].PostalAddress[2]);
            Assert.Null(reverseGeocodeResponse.Hits[0].VanityAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].GeographicAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddressResult);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddress);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].Links);
            Assert.Equal(1, reverseGeocodeResponse.Hits[0].Links.Length);
            Assert.IsType<Model.GetEcadData.Link>(reverseGeocodeResponse.Hits[0].Links[0]);
        }

        [Fact]
        public async Task ReverseGeocodeAsync_LongitudeEqualsMinus6Point271796_LatitudeEquals53Point343761_ReturnsValidResponse()
        {
            const double longitude = -6.271796;
            const double latitude = 53.343761;
            const double maxDistance = 100;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.ReverseGeocode.Request(latitude: latitude, longitude: longitude, maxDistance: maxDistance, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false, addressProfileName: null);

            var reverseGeocodeResponse = await autoaddressClient.ReverseGeocodeAsync(request);

            Assert.NotNull(reverseGeocodeResponse);
            Assert.NotNull(reverseGeocodeResponse.Hits);
            Assert.Equal(1, reverseGeocodeResponse.Hits.Length);
            Assert.Equal(1401182204, reverseGeocodeResponse.Hits[0].AddressId);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].PostalAddress);
            Assert.Equal(3, reverseGeocodeResponse.Hits[0].PostalAddress.Length);
            Assert.Equal("INNS COURT", reverseGeocodeResponse.Hits[0].PostalAddress[0]);
            Assert.Equal("WINETAVERN STREET", reverseGeocodeResponse.Hits[0].PostalAddress[1]);
            Assert.Equal("DUBLIN 8", reverseGeocodeResponse.Hits[0].PostalAddress[2]);
            Assert.Null(reverseGeocodeResponse.Hits[0].VanityAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].GeographicAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddressResult);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddress);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].Links);
            Assert.Equal(1, reverseGeocodeResponse.Hits[0].Links.Length);
            Assert.IsType<Model.GetEcadData.Link>(reverseGeocodeResponse.Hits[0].Links[0]);
        }

        [Fact]
        public void GetGbPostcodeData_BT11Space8QT_ReturnsValidResponse()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var response = autoaddressClient.GetGbPostcodeData(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetGbPostcodeData.ReturnCode.PostcodeValid, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Wgs84);
            Assert.NotNull(response.SpatialInfo.Wgs84.Location);
            Assert.True(response.SpatialInfo.Wgs84.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Wgs84.Location.Longitude < 0);
        }

        [Fact]
        public async Task GetGbPostcodeDataAsync_BT11Space8QT_ReturnsValidResponse()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var response = await autoaddressClient.GetGbPostcodeDataAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Autoaddress2_0.Model.GetGbPostcodeData.ReturnCode.PostcodeValid, response.Result);
            Assert.Equal(postcode, response.Postcode);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Wgs84);
            Assert.NotNull(response.SpatialInfo.Wgs84.Location);
            Assert.True(response.SpatialInfo.Wgs84.Location.Latitude > 0);
            Assert.True(response.SpatialInfo.Wgs84.Location.Longitude < 0);
        }

        [Fact]
        public void GetGbPostcodeData_BT11Space8QTThenSelectSelfLink_ReturnsValidResponses()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var firstResponse = autoaddressClient.GetGbPostcodeData(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.True(firstResponse.Links.Length > 0);
            var link = firstResponse.Links.OfType<Model.GetGbPostcodeData.Link>().First();

            var secondResponse = autoaddressClient.GetGbPostcodeData(link);

            Assert.NotNull(secondResponse);
            Assert.Equal(firstResponse.Result, secondResponse.Result);
            Assert.Equal(firstResponse.Postcode, secondResponse.Postcode);
        }

        [Fact]
        public void MapId_EcadId_1401182204_ReturnsValidResponse()
        {
            const int ecadId = 1401182204;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.MapId.Request(ecadId: ecadId);

            var response = autoaddressClient.MapId(request);

            Assert.NotNull(response);
            Assert.Equal(Model.MapId.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal("B:50596412", response.GeoDirectoryId);
            Assert.Null(response.EcadId);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrWhiteSpace(response.Input.GeoDirectoryVersion));
        }

        [Fact]
        public void MapId_GeoDirectoryId_BColon50596412_ReturnsValidResponse()
        {
            const string geoDirectoryId = "B:50596412";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.MapId.Request(geoDirectoryId: geoDirectoryId);

            var response = autoaddressClient.MapId(request);

            Assert.NotNull(response);
            Assert.Equal(Model.MapId.ReturnCode.GeoDirectoryIdValid, response.Result);
            Assert.Equal(1401182204, response.EcadId);
            Assert.Null(response.GeoDirectoryId);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrWhiteSpace(response.Input.GeoDirectoryVersion));
        }

        [Fact]
        public async Task MapIdAsync_EcadId_1401182204_ReturnsValidResponse()
        {
            const int ecadId = 1401182204;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.MapId.Request(ecadId: ecadId);

            var response = await autoaddressClient.MapIdAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Model.MapId.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal("B:50596412", response.GeoDirectoryId);
            Assert.Null(response.EcadId);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrWhiteSpace(response.Input.GeoDirectoryVersion));
        }

        [Fact]
        public async Task MapIdAsync_GeoDirectoryId_BColon50596412_ReturnsValidResponse()
        {
            const string geoDirectoryId = "B:50596412";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.MapId.Request(geoDirectoryId: geoDirectoryId);

            var response = await autoaddressClient.MapIdAsync(request);

            Assert.NotNull(response);
            Assert.Equal(Model.MapId.ReturnCode.GeoDirectoryIdValid, response.Result);
            Assert.Equal(1401182204, response.EcadId);
            Assert.Null(response.GeoDirectoryId);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrWhiteSpace(response.Input.GeoDirectoryVersion));
        }

        [Fact]
        public void GetEcadData_1701984269_AdministrativeInfo_2017_ReturnsValidResponse()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId, false, txn: null, administrativeInfo: "2017");

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.Equal("2017", response.Input.AdministrativeInfo);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.Equal(ecadId, response.EcadId);
            Assert.Equal(Autoaddress2_0.Model.GetEcadData.EcadIdStatus.Current, response.EcadIdStatus);
            Assert.Equal(2150, response.AddressTypeId);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.Equal(1400247786, response.AdministrativeInfo.EcadId);
            Assert.Equal("2017", response.AdministrativeInfo.Release);
            Assert.Equal(16, response.AdministrativeInfo.LaId);
            Assert.Equal(167029, response.AdministrativeInfo.DedId);
            Assert.Equal(8577, response.AdministrativeInfo.SmallAreaId);
            Assert.Equal(160648, response.AdministrativeInfo.TownlandId);
            Assert.Equal(1001000020, response.AdministrativeInfo.CountyId);
            Assert.Equal(false, response.AdministrativeInfo.Gaeltacht);
        }
        [Fact]
        public async Task FindAddress_IdOfBuildingThatHasStatus2_ReturnsPostcodeNotAvailableNoMailDelivery()
        {
            const string address = null;
            const int addressId = 1400021286;
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null,
                                                                                   addressId: addressId);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeNotAvailable, response.Result);
            Assert.Equal(Autoaddress2_0.Model.FindAddress.PostcodeNotAvailable.NoMailDelivery, response.PostcodeNotAvailable);
        }

        [Fact]
        public async Task FindAddressController_IdOfBuildingThatHasStatus3_ReturnsPostcodeNotAvailableNoRoutingKey()
        {
            const string address = null;
            const int addressId = 1401939590;
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null,
                                                                                   addressId: addressId);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeNotAvailable, response.Result);
            Assert.Equal(Autoaddress2_0.Model.FindAddress.PostcodeNotAvailable.NoRoutingKey, response.PostcodeNotAvailable);
        }

        [Fact]
        public async Task FindAddressController_IdOfBuildingThatHasSpatialAccuracy1ButNoEircode_ReturnsPostcodeNotAvailableNoCoordinates()
        {
            const string address = null;
            const int addressId = 1401935829;
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null,
                                                                                   addressId: addressId);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.Null(response.PostcodeNotAvailable);
        }
        [Fact]
        public async Task FindAddressController_AddressOfBuildingThatHasStatus2_ReturnsPostcodeNotAvailableNoMailDelivery()
        {
            const string address = "42 Wickham Street, Limerick";
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeNotAvailable, response.Result);
            Assert.Equal(Autoaddress2_0.Model.FindAddress.PostcodeNotAvailable.NoMailDelivery, response.PostcodeNotAvailable);
        }

        [Fact]
        public async Task FindAddressController_AddressOfBuildingThatHasStatus3_ReturnsPostcodeNotAvailableNoRoutingKey()
        {
            const string address = "5 The Mews, Mary Street, Cork";
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeNotAvailable, response.Result);
            Assert.Equal(Autoaddress2_0.Model.FindAddress.PostcodeNotAvailable.NoRoutingKey, response.PostcodeNotAvailable);
        }

        [Fact]
        public async Task FindAddressController_AddressOfBuildingThatHasStatus1ButNoEircode_ReturnsPostcodeNotAvailableNoCoordinates()
        {
            const string address = "61 Marrsfield Avenue, Clongriff, D13";
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address,
                                                                                   language: Language.EN,
                                                                                   country: Country.IE,
                                                                                   limit: 20,
                                                                                   geographicAddress: false,
                                                                                   vanityMode: false,
                                                                                   addressElements: false,
                                                                                   addressProfileName: null);

            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Input);
            Assert.True(!string.IsNullOrEmpty(response.Input.Txn));
            Assert.Equal(Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.Null(response.PostcodeNotAvailable);
        }
    }
}
