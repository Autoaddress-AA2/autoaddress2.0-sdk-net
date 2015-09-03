using System;
using System.Linq;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.PostcodeLookup;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientPostcodeLookupAsyncLinkExample1
    {
        public static async void Main()
        {
            const string postcode = "D08XY00";
            var autoaddressClient = new AutoaddressClient();
            var request = new Request(postcode, Language.EN, Country.IE, 20);

            var response1 = await autoaddressClient.PostcodeLookupAsync(request);

            Console.WriteLine("response1.Result = {0}", response1.Result);
            Console.WriteLine("response1.MatchLevel = {0}", response1.MatchLevel);
            Console.WriteLine("response1.AddressType = {0}", response1.AddressType);
            Console.WriteLine("response1.AddressId = {0}", response1.AddressId);
            Console.WriteLine("response1.Postcode = {0}", response1.Postcode);
            Console.WriteLine("response1.PostalAddress = {0}", string.Join(",", response1.PostalAddress));
            Console.WriteLine("response1.Options[0].DisplayName = {0}", response1.Options[0].DisplayName);
            Console.WriteLine("response1.Options[1].DisplayName = {0}", response1.Options[1].DisplayName);
            Console.WriteLine("response1.Options[2].DisplayName = {0}", response1.Options[2].DisplayName);

            var response2 = await autoaddressClient.PostcodeLookupAsync(response1.Options[2].Links.OfType<Model.PostcodeLookup.Link>().First());
            
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
// response1.AddressId = 1702105351
// response1.Postcode = D08XY00
// response1.PostalAddress = 4 INNS COURT,WINETAVERN STREET,DUBLIN 8
// response1.Options[0].DisplayName = 4 INNS COURT, WINETAVERN STREET, DUBLIN 8
// response1.Options[1].DisplayName = BIZMAPS LIMITED, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8
// response1.Options[2].DisplayName = GAMMA, 4 INNS COURT, WINETAVERN STREET, DUBLIN 8
// response2.Result = ValidPostcode
// response2.MatchLevel = Organisation
// response2.AddressType = Organisation
// response2.AddressId = 1900187606
// response2.Postcode = D08XY00
// response2.PostalAddress = GAMMA,4 INNS COURT,WINETAVERN STREET,DUBLIN 8