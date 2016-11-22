using System.Linq;
using System.Threading.Tasks;
using Autoaddress.Autoaddress2_0.Model;
using NUnit.Framework;

namespace Autoaddress.Autoaddress2_0.Test.Integration
{
    [TestFixture]
    public class AutoaddressClientTest
    {
        [Test]
        public void FindAddress_IE_8SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);
            
            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddress_IE_8SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne, A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeValidated, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddress_IE_8SilverBirchesDunboyneA86VC05_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne, A86VC05";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAmended, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddress_IE_9SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "9 Silver Birches, Dunboyne, A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.AddressAmendedToMatchPostcode, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddress_IE_8SilverBirchesDunboyneInvalidLicenceKey_ThrowsAutoaddressException()
        {
            const string licenceKey = "InvalidLicenceKey";
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(licenceKey);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            AutoaddressException autoaddressException = Assert.Throws<AutoaddressException>(() => autoaddressClient.FindAddress(request));
            Assert.AreEqual(ErrorType.InvalidLicenceKey, autoaddressException.ErrorType);
        }

        [Test]
        public void FindAddress_IE_8SilverBirchesDunboyneUseKeyFromAppConfig_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddress_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
        }

        [Test]
        public void FindAddress_IE_SilverBirchesDunboyneThenSelectFirstOption_ReturnsValidResponses()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.FindAddress(request);

            Assert.NotNull(firstResponse);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.IncompleteAddressEntered, firstResponse.Result);
            Assert.NotNull(firstResponse.Options);
            var option = firstResponse.Options[0];
            var nextLink = option.Links.OfType<Model.FindAddress.Link>().First();

            var secondResponse = autoaddressClient.FindAddress(nextLink);
            Assert.NotNull(secondResponse);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.IncompleteAddressEntered, secondResponse.Result);
            Assert.NotNull(secondResponse.Options);
        }

        [Test]
        public void FindAddress_IE_SilverBirchesDunboyneThenSelectSelfLink_ReturnsValidResponses()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.FindAddress(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.Greater(firstResponse.Links.Length, 0);
            var link = firstResponse.Links.OfType<Model.FindAddress.Link>().First();

            var secondResponse = autoaddressClient.FindAddress(link);

            Assert.NotNull(secondResponse);
            Assert.AreEqual(firstResponse.Result, secondResponse.Result);
            Assert.AreEqual(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Test]
        public void FindAddress_IE_1WoodlandsRoadCabinteelyDublin18_vanityModeEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string address = "1 Woodlands Road, Cabinteely, Dublin 18";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: true, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("A96E2R8", response.Postcode);
            Assert.IsNull(response.Unmatched);
            Assert.IsNull(response.UnmatchedAddressElements);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual("1 WOODLANDS ROAD", response.PostalAddress[0]);
            Assert.AreEqual("GLENAGEARY", response.PostalAddress[1]);
            Assert.AreEqual("CO. DUBLIN", response.PostalAddress[2]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.AreEqual(4, response.PostalAddressElements.Length);
            Assert.AreEqual("1", response.PostalAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingNumber, response.PostalAddressElements[0].Type);
            Assert.AreEqual(1401042441, response.PostalAddressElements[0].AddressId);
            Assert.AreEqual("WOODLANDS ROAD", response.PostalAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.Thoroughfare, response.PostalAddressElements[1].Type);
            Assert.AreEqual(1200029775, response.PostalAddressElements[1].AddressId);
            Assert.AreEqual("GLENAGEARY", response.PostalAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.AreEqual(1100000090, response.PostalAddressElements[2].AddressId);
            Assert.AreEqual("CO. DUBLIN", response.PostalAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.County, response.PostalAddressElements[3].Type);
            Assert.AreEqual(1001000025, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.VanityAddress);
            Assert.AreEqual("1 Woodlands Road", response.VanityAddress[0]);
            Assert.AreEqual("Cabinteely", response.VanityAddress[1]);
            Assert.AreEqual("Dublin 18", response.VanityAddress[2]);
            Assert.NotNull(response.VanityAddressElements);
            Assert.AreEqual(4, response.VanityAddressElements.Length);
            Assert.AreEqual("1", response.VanityAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingNumber, response.VanityAddressElements[0].Type);
            Assert.AreEqual(1401042441, response.VanityAddressElements[0].AddressId);
            Assert.AreEqual("Woodlands Road", response.VanityAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.Thoroughfare, response.VanityAddressElements[1].Type);
            Assert.AreEqual(1200029775, response.VanityAddressElements[1].AddressId);
            Assert.AreEqual("Cabinteely", response.VanityAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.UrbanArea, response.VanityAddressElements[2].Type);
            Assert.AreEqual(1110029573, response.VanityAddressElements[2].AddressId);
            Assert.AreEqual("Dublin 18", response.VanityAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.DublinPostalArea, response.VanityAddressElements[3].Type);
            Assert.AreEqual(1100000017, response.VanityAddressElements[3].AddressId);
        }

        [Test]
        public void FindAddress_IE_TerminalBuildingShannonAirportShannonCoDotClare_geographicAddressEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string address = "Terminal Building, Shannon Airport, Shannon, Co. Clare";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: true, vanityMode: false, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("V14NX04", response.Postcode);
            Assert.IsNull(response.Unmatched);
            Assert.IsNull(response.UnmatchedAddressElements);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual("TERMINAL BUILDING", response.PostalAddress[0]);
            Assert.AreEqual("SHANNON AIRPORT", response.PostalAddress[1]);
            Assert.AreEqual("SHANNON", response.PostalAddress[2]);
            Assert.AreEqual("LIMERICK", response.PostalAddress[3]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.AreEqual(4, response.PostalAddressElements.Length);
            Assert.AreEqual("TERMINAL BUILDING", response.PostalAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingName, response.PostalAddressElements[0].Type);
            Assert.AreEqual(1401207129, response.PostalAddressElements[0].AddressId);
            Assert.AreEqual("SHANNON AIRPORT", response.PostalAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.RuralLocality, response.PostalAddressElements[1].Type);
            Assert.AreEqual(1110026207, response.PostalAddressElements[1].AddressId);
            Assert.AreEqual("SHANNON", response.PostalAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.AreEqual(1100000099, response.PostalAddressElements[2].AddressId);
            Assert.AreEqual("LIMERICK", response.PostalAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.City, response.PostalAddressElements[3].Type);
            Assert.AreEqual(1100000030, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.GeographicAddress);
            Assert.AreEqual("TERMINAL BUILDING", response.GeographicAddress[0]);
            Assert.AreEqual("SHANNON AIRPORT", response.GeographicAddress[1]);
            Assert.AreEqual("SHANNON", response.GeographicAddress[2]);
            Assert.AreEqual("CO. CLARE", response.GeographicAddress[3]);
            Assert.NotNull(response.GeographicAddressElements);
            Assert.AreEqual(4, response.GeographicAddressElements.Length);
            Assert.AreEqual("TERMINAL BUILDING", response.GeographicAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingName, response.GeographicAddressElements[0].Type);
            Assert.AreEqual(1401207129, response.GeographicAddressElements[0].AddressId);
            Assert.AreEqual("SHANNON AIRPORT", response.GeographicAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.RuralLocality, response.GeographicAddressElements[1].Type);
            Assert.AreEqual(1110026207, response.GeographicAddressElements[1].AddressId);
            Assert.AreEqual("SHANNON", response.GeographicAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.Town, response.GeographicAddressElements[2].Type);
            Assert.AreEqual(1100000099, response.GeographicAddressElements[2].AddressId);
            Assert.AreEqual("CO. CLARE", response.GeographicAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.County, response.GeographicAddressElements[3].Type);
            Assert.AreEqual(1001000003, response.GeographicAddressElements[3].AddressId);
            Assert.IsNull(response.VanityAddress);
        }

        [Test]
        public async Task FindAddressAsync_IE_8SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("A86VC04", response.Postcode);
        }

        [Test]
        public void FindAddressAsync_IE_8SilverBirchesDunboyneInvalidLicenceKey_ThrowsAutoaddressException()
        {
            const string licenceKey = "InvalidLicenceKey";
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(licenceKey);
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            Assert.ThrowsAsync<AutoaddressException>(async () => await autoaddressClient.FindAddressAsync(request));
        }

        [Test]
        public async Task FindAddressAsync_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.FindAddressAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
        }

        [Test]
        public void PostcodeLookup_IE_A86VC04_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(4, response.PostalAddress.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress[3]);
        }

        [Test]
        public void PostcodeLookup_IE_A96E2R8_vanityModeEqualsTrue_addressElementsEqualsTrue_ReturnsValidResponse()
        {
            const string postcode = "A96E2R8";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: true, addressElements: true, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.AreEqual("A96E2R8", response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual("1 WOODLANDS ROAD", response.PostalAddress[0]);
            Assert.AreEqual("GLENAGEARY", response.PostalAddress[1]);
            Assert.AreEqual("CO. DUBLIN", response.PostalAddress[2]);
            Assert.NotNull(response.PostalAddressElements);
            Assert.AreEqual(4, response.PostalAddressElements.Length);
            Assert.AreEqual("1", response.PostalAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingNumber, response.PostalAddressElements[0].Type);
            Assert.AreEqual(1401042441, response.PostalAddressElements[0].AddressId);
            Assert.AreEqual("WOODLANDS ROAD", response.PostalAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.Thoroughfare, response.PostalAddressElements[1].Type);
            Assert.AreEqual(1200029775, response.PostalAddressElements[1].AddressId);
            Assert.AreEqual("GLENAGEARY", response.PostalAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.Town, response.PostalAddressElements[2].Type);
            Assert.AreEqual(1100000090, response.PostalAddressElements[2].AddressId);
            Assert.AreEqual("CO. DUBLIN", response.PostalAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.County, response.PostalAddressElements[3].Type);
            Assert.AreEqual(1001000025, response.PostalAddressElements[3].AddressId);
            Assert.NotNull(response.VanityAddress);
            Assert.AreEqual("1 Woodlands Road", response.VanityAddress[0]);
            Assert.AreEqual("Dun Laoghaire", response.VanityAddress[1]);
            Assert.AreEqual("Co. Dublin", response.VanityAddress[2]);
            Assert.NotNull(response.VanityAddressElements);
            Assert.AreEqual(4, response.VanityAddressElements.Length);
            Assert.AreEqual("1", response.VanityAddressElements[0].Value);
            Assert.AreEqual(AddressElementType.BuildingNumber, response.VanityAddressElements[0].Type);
            Assert.AreEqual(1401042441, response.VanityAddressElements[0].AddressId);
            Assert.AreEqual("Woodlands Road", response.VanityAddressElements[1].Value);
            Assert.AreEqual(AddressElementType.Thoroughfare, response.VanityAddressElements[1].Type);
            Assert.AreEqual(1200029775, response.VanityAddressElements[1].AddressId);
            Assert.AreEqual("Dun Laoghaire", response.VanityAddressElements[2].Value);
            Assert.AreEqual(AddressElementType.Locality, response.VanityAddressElements[2].Type);
            Assert.AreEqual(1100000131, response.VanityAddressElements[2].AddressId);
            Assert.AreEqual("Co. Dublin", response.VanityAddressElements[3].Value);
            Assert.AreEqual(AddressElementType.County, response.VanityAddressElements[3].Type);
            Assert.AreEqual(1001000025, response.VanityAddressElements[3].AddressId);
        }

        [Test]
        public void PostcodeLookup_IE_A86VC04ThenSelectSelfLink_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.PostcodeLookup(request);
            
            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.Greater(firstResponse.Links.Length, 0);
            var link = firstResponse.Links.OfType<Model.PostcodeLookup.Link>().First();

            var secondResponse = autoaddressClient.PostcodeLookup(link);

            Assert.NotNull(secondResponse);
            Assert.AreEqual(firstResponse.Result, secondResponse.Result);
            Assert.AreEqual(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Test]
        public void PostcodeLookup_IE_D08XY00_ReturnsValidResponse()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(3, response.PostalAddress.Length);
            Assert.AreEqual("4 INNS COURT", response.PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", response.PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", response.PostalAddress[2]);
            Assert.IsNotNull(response.Options);
            Assert.AreEqual(3, response.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Test]
        public void PostcodeLookup_IE_D08XY00ThenSelectGammaFromOptions_ReturnsValidResponses()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = autoaddressClient.PostcodeLookup(request);

            Assert.NotNull(firstResponse);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.AreEqual(postcode, firstResponse.Postcode);
            Assert.NotNull(firstResponse.PostalAddress);
            Assert.AreEqual(3, firstResponse.PostalAddress.Length);
            Assert.AreEqual("4 INNS COURT", firstResponse.PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", firstResponse.PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", firstResponse.PostalAddress[2]);
            Assert.IsNotNull(firstResponse.Options);
            Assert.AreEqual(3, firstResponse.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[2].DisplayName);
            Assert.NotNull(firstResponse.Options[2].Links);
            Assert.Greater(firstResponse.Options[2].Links.Length, 0);
            Assert.NotNull(firstResponse.Options[2].Links[0]);

            var secondResponse = autoaddressClient.PostcodeLookup(firstResponse.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.AreEqual(postcode, firstResponse.Postcode);
            Assert.AreEqual(AddressType.Organisation, secondResponse.AddressType);
        }

        [Test]
        public void PostcodeLookup_IE_F94H289_ReturnsValidResponse()
        {
            const string postcode = "F94H289";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.RetiredPostcode, response.Result);
            Assert.AreEqual("F94H289", response.Postcode);
            Assert.AreEqual(MatchLevel.Unknown, response.MatchLevel);
            Assert.IsNull(response.AddressType);
            Assert.IsNull(response.AddressId);
            Assert.IsNull(response.AddressType);
            Assert.IsNull(response.PostalAddress);
            Assert.IsNull(response.PostalAddressElements);
            Assert.IsNull(response.VanityAddress);
            Assert.IsNull(response.VanityAddressElements);
            Assert.IsNull(response.ReformattedAddressResult);
            Assert.IsNull(response.ReformattedAddress);
            Assert.AreEqual(0, response.TotalOptions);
            Assert.IsNotNull(response.Options);
            Assert.AreEqual(0, response.Options.Length);
        }

        [Test]
        public void PostcodeLookup_IE_Y35F9KX_ReturnsValidResponse()
        {
            const string postcode = "Y35F9KX";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ChangedPostcode, response.Result);
            Assert.NotNull(response.Postcode);
            Assert.AreEqual("Y35TD51", response.Postcode);
            Assert.AreEqual(1702151625, response.AddressId);
            Assert.AreEqual(MatchLevel.AddressPoint, response.MatchLevel);
            Assert.AreEqual(AddressType.ResidentialAddressPoint, response.AddressType);
            Assert.IsNotNull(response.PostalAddress);
            Assert.AreEqual(3, response.PostalAddress.Length);
            Assert.AreEqual("22 HILLCREST", response.PostalAddress[0]);
            Assert.AreEqual("MULGANNON", response.PostalAddress[1]);
            Assert.AreEqual("WEXFORD", response.PostalAddress[2]);
            Assert.IsNull(response.PostalAddressElements);
            Assert.IsNull(response.VanityAddress);
            Assert.IsNull(response.VanityAddressElements);
            Assert.AreEqual(0, response.TotalOptions);
            Assert.IsNotNull(response.Options);
            Assert.AreEqual(0, response.Options.Length);
        }

        [Test]
        public void FindAddress_NI_9AvocaCloseBelfast_ReturnsValidResponse()
        {
            const string address = "9 Avoca Close, Belfast";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress.Autoaddress2_0.Model.FindAddress.Request(address: address, language: Language.EN, country: Country.NI, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.FindAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, response.Result);
            Assert.AreEqual("BT11 8QT", response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(2, response.PostalAddress.Length);
            Assert.AreEqual("9 AVOCA CLOSE", response.PostalAddress[0]);
            Assert.AreEqual("BELFAST", response.PostalAddress[1]);
        }

        [Test]
        public async Task PostcodeLookupAsync_IE_A86VC04_ReturnsValidResponse()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(4, response.PostalAddress.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress[3]);
        }

        [Test]
        public async Task PostcodeLookupAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(3, response.PostalAddress.Length);
            Assert.AreEqual("4 INNS COURT", response.PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", response.PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", response.PostalAddress[2]);
            Assert.IsNotNull(response.Options);
            Assert.AreEqual(3, response.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Test]
        public async Task PostcodeLookupAsync_IE_D08XY00ThenSelectGammaFromOptions_ReturnsValidResponses()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.PostcodeLookup.Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var firstResponse = await autoaddressClient.PostcodeLookupAsync(request);

            Assert.NotNull(firstResponse);
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.AreEqual(postcode, firstResponse.Postcode);
            Assert.NotNull(firstResponse.PostalAddress);
            Assert.AreEqual(3, firstResponse.PostalAddress.Length);
            Assert.AreEqual("4 INNS COURT", firstResponse.PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", firstResponse.PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", firstResponse.PostalAddress[2]);
            Assert.IsNotNull(firstResponse.Options);
            Assert.AreEqual(3, firstResponse.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", firstResponse.Options[2].DisplayName);
            Assert.NotNull(firstResponse.Options[2].Links);
            Assert.Greater(firstResponse.Options[2].Links.Length, 0);
            Assert.NotNull(firstResponse.Options[2].Links[0]);

            var secondResponse = await autoaddressClient.PostcodeLookupAsync(firstResponse.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            Assert.AreEqual(Autoaddress2_0.Model.PostcodeLookup.ReturnCode.ValidPostcode, firstResponse.Result);
            Assert.AreEqual(postcode, firstResponse.Postcode);
            Assert.AreEqual(AddressType.Organisation, secondResponse.AddressType);
        }

        [Test]
        public void VerifyAddress_IE_8SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var response = autoaddressClient.VerifyAddress(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.VerifyAddress.ReturnCode.AddressAndEircodeMatch, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(4, response.PostalAddress.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress[3]);
        }

        [Test]
        public void VerifyAddress_IE_8SilverBirchesDunboyneA86VC04ThenSelectSelfLink_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var firstResponse = autoaddressClient.VerifyAddress(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.Greater(firstResponse.Links.Length, 0);
            var link = firstResponse.Links.OfType<Model.VerifyAddress.Link>().First();

            var secondResponse = autoaddressClient.VerifyAddress(link);

            Assert.NotNull(secondResponse);
            Assert.AreEqual(firstResponse.Result, secondResponse.Result);
            Assert.AreEqual(firstResponse.AddressId, secondResponse.AddressId);
        }

        [Test]
        public async Task VerifyAddressAsync_IE_8SilverBirchesDunboyneA86VC04_ReturnsValidResponse()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.VerifyAddress.Request(postcode: postcode, address: address, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false);

            var response = await autoaddressClient.VerifyAddressAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.VerifyAddress.ReturnCode.AddressAndEircodeMatch, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.PostalAddress);
            Assert.AreEqual(4, response.PostalAddress.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress[3]);
        }

        [Test]
        public void GetEcadData_1701984269_ReturnsValidResponse()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.AreEqual(ecadId, response.EcadId);
            Assert.AreEqual(2150, response.AddressTypeId);
            Assert.NotNull(response.EircodeInfo);
            Assert.AreEqual(ecadId, response.EircodeInfo.EcadId);
            Assert.AreEqual("A86VC04", response.EircodeInfo.Eircode);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.AreEqual(4, response.PostalAddress.English.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress.English[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress.English[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress.English[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress.English[3]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.AreEqual(4, response.PostalAddress.Irish.Length);
            Assert.AreEqual("8 NA BEITHEANNA GEALA", response.PostalAddress.Irish[0]);
            Assert.AreEqual("FEIRM AN MHUILINN", response.PostalAddress.Irish[1]);
            Assert.AreEqual("DÚN BÚINNE", response.PostalAddress.Irish[2]);
            Assert.AreEqual("CO. NA MÍ", response.PostalAddress.Irish[3]);
            Assert.NotNull(response.GeographicAddress);
            Assert.NotNull(response.GeographicAddress.English);
            Assert.AreEqual(4, response.GeographicAddress.English.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.GeographicAddress.English[0]);
            Assert.AreEqual("MILLFARM", response.GeographicAddress.English[1]);
            Assert.AreEqual("DUNBOYNE", response.GeographicAddress.English[2]);
            Assert.AreEqual("CO. MEATH", response.GeographicAddress.English[3]);
            Assert.NotNull(response.GeographicAddress.Irish);
            Assert.AreEqual(4, response.GeographicAddress.Irish.Length);
            Assert.AreEqual("8 NA BEITHEANNA GEALA", response.GeographicAddress.Irish[0]);
            Assert.AreEqual("FEIRM AN MHUILINN", response.GeographicAddress.Irish[1]);
            Assert.AreEqual("DÚN BÚINNE", response.GeographicAddress.Irish[2]);
            Assert.AreEqual("CO. NA MÍ", response.GeographicAddress.Irish[3]);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.AreEqual(1400247786, response.AdministrativeInfo.EcadId);
            Assert.AreEqual(16, response.AdministrativeInfo.LaId);
            Assert.AreEqual(167029, response.AdministrativeInfo.DedId);
            Assert.AreEqual(14588, response.AdministrativeInfo.SmallAreaId);
            Assert.AreEqual(160648, response.AdministrativeInfo.TownlandId);
            Assert.AreEqual(1001000020, response.AdministrativeInfo.CountyId);
            Assert.AreEqual(false, response.AdministrativeInfo.Gaeltacht);
            Assert.NotNull(response.BuildingInfo);
            Assert.AreEqual(1400247786, response.BuildingInfo.EcadId);
            Assert.AreEqual(1, response.BuildingInfo.BuildingTypeId);
            Assert.AreEqual(false, response.BuildingInfo.UnderConstruction);
            Assert.AreEqual("R", response.BuildingInfo.BuildingUse);
            Assert.AreEqual(false, response.BuildingInfo.Vacant);
            Assert.AreEqual(null, response.BuildingInfo.HolidayHome);
            Assert.IsNull(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.Greater(response.SpatialInfo.Etrs89.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.Location.Longitude, 0);
            Assert.IsNull(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.Location);
            Assert.Greater(response.SpatialInfo.Ing.Location.Easting, 0);
            Assert.Greater(response.SpatialInfo.Ing.Location.Northing, 0);
            Assert.IsNull(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.Location);
            Assert.Greater(response.SpatialInfo.Itm.Location.Easting, 0);
            Assert.Greater(response.SpatialInfo.Itm.Location.Northing, 0);
            Assert.IsNull(response.SpatialInfo.Itm.BoundingBox);
            Assert.AreEqual("3", response.SpatialInfo.SpatialAccuracy);
            Assert.IsNotNull(response.RelatedEcadIds);
            Assert.IsNull(response.RelatedEcadIds.AddressPointEcadId);
            Assert.AreEqual(1400247786, response.RelatedEcadIds.BuildingEcadId);
            Assert.IsNull(response.RelatedEcadIds.BuildingGroupEcadId);
            Assert.IsNotNull(response.RelatedEcadIds.ThoroughfareEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.ThoroughfareEcadIds.Length);
            Assert.AreEqual(1200021757, response.RelatedEcadIds.ThoroughfareEcadIds[0]);
            Assert.IsNotNull(response.RelatedEcadIds.LocalityEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.LocalityEcadIds.Length);
            Assert.AreEqual(1110026424, response.RelatedEcadIds.LocalityEcadIds[0]);
            Assert.IsNotNull(response.RelatedEcadIds.PostTownEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.PostTownEcadIds.Length);
            Assert.AreEqual(1100000117, response.RelatedEcadIds.PostTownEcadIds[0]);
            Assert.IsNotNull(response.RelatedEcadIds.PostCountyEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.PostCountyEcadIds.Length);
            Assert.AreEqual(1001000020, response.RelatedEcadIds.PostCountyEcadIds[0]);
            Assert.IsNotNull(response.RelatedEcadIds.GeographicCountyEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.GeographicCountyEcadIds.Length);
            Assert.AreEqual(1001000020, response.RelatedEcadIds.GeographicCountyEcadIds[0]);
        }

        [Test]
        public void GetEcadData_1701984269ThenSelectSelfLink_ReturnsValidResponses()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId);

            var firstResponse = autoaddressClient.GetEcadData(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.Greater(firstResponse.Links.Length, 0);
            var link = firstResponse.Links.OfType<Model.GetEcadData.Link>().First();

            var secondResponse = autoaddressClient.GetEcadData(link);

            Assert.NotNull(secondResponse);
            Assert.AreEqual(firstResponse.Result, secondResponse.Result);
            Assert.AreEqual(firstResponse.EcadId, secondResponse.EcadId);
        }

        [Test]
        public void GetEcadData_1200003223_ReturnsValidResponse()
        {
            const int ecadId = 1200003223;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId);

            var response = autoaddressClient.GetEcadData(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.AreEqual(ecadId, response.EcadId);
            Assert.AreEqual(3100, response.AddressTypeId);
            Assert.IsNull(response.EircodeInfo);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.AreEqual(2, response.PostalAddress.English.Length);
            Assert.AreEqual("DAME STREET", response.PostalAddress.English[0]);
            Assert.AreEqual("DUBLIN 2", response.PostalAddress.English[1]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.AreEqual(2, response.PostalAddress.Irish.Length);
            Assert.AreEqual("SRÁID AN DÁMA", response.PostalAddress.Irish[0]);
            Assert.AreEqual("BAILE ÁTHA CLIATH 2", response.PostalAddress.Irish[1]);
            Assert.IsNotNull(response.AdministrativeInfo);
            Assert.AreEqual(ecadId, response.AdministrativeInfo.EcadId);
            Assert.AreEqual(268140, response.AdministrativeInfo.DedId);
            Assert.AreEqual(false, response.AdministrativeInfo.Gaeltacht);
            Assert.AreEqual(29, response.AdministrativeInfo.LaId);
            Assert.IsNull(response.AdministrativeInfo.SmallAreaId);
            Assert.IsNull(response.AdministrativeInfo.TownlandId);
            Assert.AreEqual(1001000025, response.AdministrativeInfo.CountyId);
            Assert.IsNull(response.BuildingInfo);
            Assert.IsNull(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.AreEqual(ecadId, response.SpatialInfo.EcadId);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.Greater(response.SpatialInfo.Etrs89.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.Location.Longitude, 0);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Etrs89.BoundingBox.Max);
            Assert.Greater(response.SpatialInfo.Etrs89.BoundingBox.Min.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.BoundingBox.Min.Longitude, 0);
            Assert.Greater(response.SpatialInfo.Etrs89.BoundingBox.Max.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.BoundingBox.Max.Longitude, 0);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.Greater(response.SpatialInfo.Etrs89.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.Location.Longitude, 0);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Ing.BoundingBox.Max);
            Assert.Greater(response.SpatialInfo.Ing.BoundingBox.Min.Easting, 0);
            Assert.Greater(response.SpatialInfo.Ing.BoundingBox.Min.Northing, 0);
            Assert.Greater(response.SpatialInfo.Ing.BoundingBox.Max.Easting, 0);
            Assert.Greater(response.SpatialInfo.Ing.BoundingBox.Max.Northing, 0);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox.Min);
            Assert.NotNull(response.SpatialInfo.Itm.BoundingBox.Max);
            Assert.Greater(response.SpatialInfo.Itm.BoundingBox.Min.Easting, 0);
            Assert.Greater(response.SpatialInfo.Itm.BoundingBox.Min.Northing, 0);
            Assert.Greater(response.SpatialInfo.Itm.BoundingBox.Max.Easting, 0);
            Assert.Greater(response.SpatialInfo.Itm.BoundingBox.Max.Northing, 0);
            Assert.AreEqual("3", response.SpatialInfo.SpatialAccuracy);
            Assert.IsNotNull(response.RelatedEcadIds);
        }

        [Test]
        public async Task GetEcadDataAsync_1701984269_ReturnsValidResponse()
        {
            const int ecadId = 1701984269;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetEcadData.Request(ecadId);

            var response = await autoaddressClient.GetEcadDataAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.GetEcadData.ReturnCode.EcadIdValid, response.Result);
            Assert.AreEqual(ecadId, response.EcadId);
            Assert.AreEqual(2150, response.AddressTypeId);
            Assert.NotNull(response.EircodeInfo);
            Assert.AreEqual(ecadId, response.EircodeInfo.EcadId);
            Assert.AreEqual("A86VC04", response.EircodeInfo.Eircode);
            Assert.NotNull(response.PostalAddress);
            Assert.NotNull(response.PostalAddress.English);
            Assert.AreEqual(4, response.PostalAddress.English.Length);
            Assert.AreEqual("8 SILVER BIRCHES", response.PostalAddress.English[0]);
            Assert.AreEqual("MILLFARM", response.PostalAddress.English[1]);
            Assert.AreEqual("DUNBOYNE", response.PostalAddress.English[2]);
            Assert.AreEqual("CO. MEATH", response.PostalAddress.English[3]);
            Assert.NotNull(response.PostalAddress.Irish);
            Assert.AreEqual(4, response.PostalAddress.Irish.Length);
            Assert.AreEqual("8 NA BEITHEANNA GEALA", response.PostalAddress.Irish[0]);
            Assert.AreEqual("FEIRM AN MHUILINN", response.PostalAddress.Irish[1]);
            Assert.AreEqual("DÚN BÚINNE", response.PostalAddress.Irish[2]);
            Assert.AreEqual("CO. NA MÍ", response.PostalAddress.Irish[3]);
            Assert.NotNull(response.AdministrativeInfo);
            Assert.AreEqual(1400247786, response.AdministrativeInfo.EcadId);
            Assert.AreEqual(16, response.AdministrativeInfo.LaId);
            Assert.AreEqual(167029, response.AdministrativeInfo.DedId);
            Assert.AreEqual(14588, response.AdministrativeInfo.SmallAreaId);
            Assert.AreEqual(160648, response.AdministrativeInfo.TownlandId);
            Assert.AreEqual(1001000020, response.AdministrativeInfo.CountyId);
            Assert.AreEqual(false, response.AdministrativeInfo.Gaeltacht);
            Assert.NotNull(response.BuildingInfo);
            Assert.AreEqual(1400247786, response.BuildingInfo.EcadId);
            Assert.AreEqual(1, response.BuildingInfo.BuildingTypeId);
            Assert.AreEqual(false, response.BuildingInfo.UnderConstruction);
            Assert.AreEqual("R", response.BuildingInfo.BuildingUse);
            Assert.AreEqual(false, response.BuildingInfo.Vacant);
            Assert.AreEqual(null, response.BuildingInfo.HolidayHome);
            Assert.IsNull(response.OrganisationInfo);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Etrs89);
            Assert.NotNull(response.SpatialInfo.Etrs89.Location);
            Assert.Greater(response.SpatialInfo.Etrs89.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Etrs89.Location.Longitude, 0);
            Assert.IsNull(response.SpatialInfo.Etrs89.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Ing);
            Assert.NotNull(response.SpatialInfo.Ing.Location);
            Assert.Greater(response.SpatialInfo.Ing.Location.Easting, 0);
            Assert.Greater(response.SpatialInfo.Ing.Location.Northing, 0);
            Assert.IsNull(response.SpatialInfo.Ing.BoundingBox);
            Assert.NotNull(response.SpatialInfo.Itm);
            Assert.NotNull(response.SpatialInfo.Itm.Location);
            Assert.Greater(response.SpatialInfo.Itm.Location.Easting, 0);
            Assert.Greater(response.SpatialInfo.Itm.Location.Northing, 0);
            Assert.IsNull(response.SpatialInfo.Itm.BoundingBox);
            Assert.AreEqual("3", response.SpatialInfo.SpatialAccuracy);
            Assert.IsNotNull(response.RelatedEcadIds);
            Assert.IsNotNull(response.RelatedEcadIds.GeographicCountyEcadIds);
            Assert.AreEqual(1, response.RelatedEcadIds.GeographicCountyEcadIds.Length);
            Assert.AreEqual(1001000020, response.RelatedEcadIds.GeographicCountyEcadIds[0]);
        }

        [Test]
        public void AutoComplete_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.AutoComplete(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.AreEqual(1, response.Options.Length);
            Assert.AreEqual("SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH", response.Options[0].DisplayName);
        }

        [Test]
        public async Task AutoCompleteAsync_IE_SilverBirchesDunboyne_ReturnsValidResponse()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.AreEqual(1, response.Options.Length);
            Assert.AreEqual("SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH", response.Options[0].DisplayName);
        }

        [Test]
        public void AutoComplete_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.AutoComplete(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.AreEqual(3, response.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Test]
        public async Task AutoCompleteAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Options);
            Assert.AreEqual(3, response.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", response.Options[2].DisplayName);
        }

        [Test]
        public void AutoCompleteThenFindAddress_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var autoCompleteResponse = autoaddressClient.AutoComplete(request);

            Assert.NotNull(autoCompleteResponse);
            Assert.NotNull(autoCompleteResponse.Options);
            Assert.AreEqual(3, autoCompleteResponse.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[2].DisplayName);

            var link = autoCompleteResponse.Options[1].Links.OfType<Model.FindAddress.Link>().First();

            var findAddressResponse = autoaddressClient.FindAddress(link);
            
            Assert.NotNull(findAddressResponse);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, findAddressResponse.Result);
            Assert.AreEqual(eircode, findAddressResponse.Postcode);
            Assert.NotNull(findAddressResponse.PostalAddress);
            Assert.AreEqual(4, findAddressResponse.PostalAddress.Length);
            Assert.AreEqual("AUTOADDRESS", findAddressResponse.PostalAddress[0]);
            Assert.AreEqual("4 INNS COURT", findAddressResponse.PostalAddress[1]);
            Assert.AreEqual("WINETAVERN STREET", findAddressResponse.PostalAddress[2]);
            Assert.AreEqual("DUBLIN 8", findAddressResponse.PostalAddress[3]);
        }

        [Test]
        public async Task AutoCompleteAsyncThenFindAddressAsync_IE_D08XY00_ReturnsValidResponse()
        {
            const string eircode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.AutoComplete.Request(address: eircode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var autoCompleteResponse = await autoaddressClient.AutoCompleteAsync(request);

            Assert.NotNull(autoCompleteResponse);
            Assert.NotNull(autoCompleteResponse.Options);
            Assert.AreEqual(3, autoCompleteResponse.Options.Length);
            Assert.AreEqual("4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[0].DisplayName);
            Assert.AreEqual("AUTOADDRESS, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[1].DisplayName);
            Assert.AreEqual("GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8", autoCompleteResponse.Options[2].DisplayName);

            var link = autoCompleteResponse.Options[1].Links.OfType<Model.FindAddress.Link>().First();

            var findAddressResponse = await autoaddressClient.FindAddressAsync(link);
            
            Assert.NotNull(findAddressResponse);
            Assert.AreEqual(Autoaddress.Autoaddress2_0.Model.FindAddress.ReturnCode.PostcodeAppended, findAddressResponse.Result);
            Assert.AreEqual(eircode, findAddressResponse.Postcode);
            Assert.NotNull(findAddressResponse.PostalAddress);
            Assert.AreEqual(4, findAddressResponse.PostalAddress.Length);
            Assert.AreEqual("AUTOADDRESS", findAddressResponse.PostalAddress[0]);
            Assert.AreEqual("4 INNS COURT", findAddressResponse.PostalAddress[1]);
            Assert.AreEqual("WINETAVERN STREET", findAddressResponse.PostalAddress[2]);
            Assert.AreEqual("DUBLIN 8", findAddressResponse.PostalAddress[3]);
        }

        [Test]
        public void ReverseGeocode_LongitudeEqualsMinus6Point271796_LatitudeEquals53Point343761_ReturnsValidResponse()
        {
            const double longitude = -6.271796;
            const double latitude = 53.343761;
            const double maxDistance = 100;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.ReverseGeocode.Request(latitude: latitude, longitude: longitude, maxDistance: maxDistance, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false, addressProfileName: null);

            var reverseGeocodeResponse = autoaddressClient.ReverseGeocode(request);

            Assert.NotNull(reverseGeocodeResponse);
            Assert.NotNull(reverseGeocodeResponse.Hits);
            Assert.AreEqual(1, reverseGeocodeResponse.Hits.Length);
            Assert.AreEqual(1401182204, reverseGeocodeResponse.Hits[0].AddressId);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].PostalAddress);
            Assert.AreEqual(3, reverseGeocodeResponse.Hits[0].PostalAddress.Length);
            Assert.AreEqual("INNS COURT", reverseGeocodeResponse.Hits[0].PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", reverseGeocodeResponse.Hits[0].PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", reverseGeocodeResponse.Hits[0].PostalAddress[2]);
            Assert.Null(reverseGeocodeResponse.Hits[0].VanityAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].GeographicAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddressResult);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddress);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].Links);
            Assert.AreEqual(1, reverseGeocodeResponse.Hits[0].Links.Length);
            Assert.IsInstanceOf(typeof(Model.GetEcadData.Link), reverseGeocodeResponse.Hits[0].Links[0]);
        }

        [Test]
        public async Task ReverseGeocodeAsync_LongitudeEqualsMinus6Point271796_LatitudeEquals53Point343761_ReturnsValidResponse()
        {
            const double longitude = -6.271796;
            const double latitude = 53.343761;
            const double maxDistance = 100;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.ReverseGeocode.Request(latitude: latitude, longitude: longitude, maxDistance: maxDistance, language: Language.EN, country: Country.IE, geographicAddress: false, vanityMode: false, addressProfileName: null);

            var reverseGeocodeResponse = await autoaddressClient.ReverseGeocodeAsync(request);

            Assert.NotNull(reverseGeocodeResponse);
            Assert.NotNull(reverseGeocodeResponse.Hits);
            Assert.AreEqual(1, reverseGeocodeResponse.Hits.Length);
            Assert.AreEqual(1401182204, reverseGeocodeResponse.Hits[0].AddressId);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].PostalAddress);
            Assert.AreEqual(3, reverseGeocodeResponse.Hits[0].PostalAddress.Length);
            Assert.AreEqual("INNS COURT", reverseGeocodeResponse.Hits[0].PostalAddress[0]);
            Assert.AreEqual("WINETAVERN STREET", reverseGeocodeResponse.Hits[0].PostalAddress[1]);
            Assert.AreEqual("DUBLIN 8", reverseGeocodeResponse.Hits[0].PostalAddress[2]);
            Assert.Null(reverseGeocodeResponse.Hits[0].VanityAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].GeographicAddress);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddressResult);
            Assert.Null(reverseGeocodeResponse.Hits[0].ReformattedAddress);
            Assert.NotNull(reverseGeocodeResponse.Hits[0].Links);
            Assert.AreEqual(1, reverseGeocodeResponse.Hits[0].Links.Length);
            Assert.IsInstanceOf(typeof(Model.GetEcadData.Link), reverseGeocodeResponse.Hits[0].Links[0]);
        }

        [Test]
        public void GetGbPostcodeData_BT11Space8QT_ReturnsValidResponse()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var response = autoaddressClient.GetGbPostcodeData(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.GetGbPostcodeData.ReturnCode.PostcodeValid, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Wgs84);
            Assert.NotNull(response.SpatialInfo.Wgs84.Location);
            Assert.Greater(response.SpatialInfo.Wgs84.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Wgs84.Location.Longitude, 0);
        }

        [Test]
        public async Task GetGbPostcodeDataAsync_BT11Space8QT_ReturnsValidResponse()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var response = await autoaddressClient.GetGbPostcodeDataAsync(request);

            Assert.NotNull(response);
            Assert.AreEqual(Autoaddress2_0.Model.GetGbPostcodeData.ReturnCode.PostcodeValid, response.Result);
            Assert.AreEqual(postcode, response.Postcode);
            Assert.NotNull(response.SpatialInfo);
            Assert.NotNull(response.SpatialInfo.Wgs84);
            Assert.NotNull(response.SpatialInfo.Wgs84.Location);
            Assert.Greater(response.SpatialInfo.Wgs84.Location.Latitude, 0);
            Assert.Less(response.SpatialInfo.Wgs84.Location.Longitude, 0);
        }

        [Test]
        public void GetGbPostcodeData_BT11Space8QTThenSelectSelfLink_ReturnsValidResponses()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.GetGbPostcodeData.Request(postcode);

            var firstResponse = autoaddressClient.GetGbPostcodeData(request);

            Assert.NotNull(firstResponse);
            Assert.NotNull(firstResponse.Links);
            Assert.Greater(firstResponse.Links.Length, 0);
            var link = firstResponse.Links.OfType<Model.GetGbPostcodeData.Link>().First();

            var secondResponse = autoaddressClient.GetGbPostcodeData(link);

            Assert.NotNull(secondResponse);
            Assert.AreEqual(firstResponse.Result, secondResponse.Result);
            Assert.AreEqual(firstResponse.Postcode, secondResponse.Postcode);
        }
    }
}
