using System;
using System.Linq;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.PostcodeLookup;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientPostcodeLookupLinkExample1
    {
        public static void Run()
        {
            const string postcode = "D02C966";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response1 = autoaddressClient.PostcodeLookup(request);

            Console.WriteLine("response1.Result = {0}", response1.Result);
            Console.WriteLine("response1.MatchLevel = {0}", response1.MatchLevel);
            Console.WriteLine("response1.AddressType = {0}", response1.AddressType);
            Console.WriteLine("response1.AddressId = {0}", response1.AddressId);
            Console.WriteLine("response1.Postcode = {0}", response1.Postcode);
            Console.WriteLine("response1.PostalAddress = {0}", string.Join(",", response1.PostalAddress));
            Console.WriteLine("response1.Options[0].DisplayName = {0}", response1.Options[0].DisplayName);
            Console.WriteLine("response1.Options[1].DisplayName = {0}", response1.Options[1].DisplayName);
            Console.WriteLine("response1.Options[2].DisplayName = {0}", response1.Options[2].DisplayName);

            var response2 = autoaddressClient.PostcodeLookup(response1.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            
            Console.WriteLine("response2.Result = {0}", response2.Result);
            Console.WriteLine("response2.MatchLevel = {0}", response2.MatchLevel);
            Console.WriteLine("response2.AddressType = {0}", response2.AddressType);
            Console.WriteLine("response2.AddressId = {0}", response2.AddressId);
            Console.WriteLine("response2.Postcode = {0}", response2.Postcode);
            Console.WriteLine("response2.PostalAddress = {0}", string.Join(",", response2.PostalAddress));
        }
    }
}

// This code example produces the following output:
// response1.Result = ValidPostcode
// response1.MatchLevel = AddressPoint
// response1.AddressType = NonResidentialAddressPoint
// response1.AddressId = 1700013666
// response1.Postcode = D02C966
// response1.PostalAddress = SAINT ANDREW STREET POST OFFICE,19-24 SAINT ANDREW STREET, DUBLIN 2
// response1.Options[0].DisplayName = SAINT ANDREW STREET POST OFFICE, 19-24 SAINT ANDREW STREET, DUBLIN 2
// response1.Options[1].DisplayName = 2ND FLOOR SAINT ANDREW STREET POST OFFICE, 19-24 SAINT ANDREW STREET, DUBLIN 2
// response1.Options[2].DisplayName = AN POST, SAINT ANDREW STREET POST OFFICE, 19-24 SAINT ANDREW STREET, DUBLIN 2
// response2.Result = ValidPostcode
// response2.MatchLevel = Organisation
// response2.AddressType = Organisation
// response2.AddressId = 1900169004
// response2.Postcode = D02C966
// response2.PostalAddress = AN POST, SAINT ANDREW STREET POST OFFICE,19-24 SAINT ANDREW STREET, DUBLIN 2
