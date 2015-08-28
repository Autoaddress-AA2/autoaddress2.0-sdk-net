using System;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.VerifyAddress;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientAsyncVerifyRequestExample1
    {
        public static async void MainAsync()
        {
            const string address = "8 Silver Birches, Dunboyne";
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient();

            var request = new Request(postcode, address, Language.EN, Country.IE);
            var response = await autoaddressClient.VerifyAddressAsync(request);

            Console.WriteLine("response.Result = {0}", response.Result);
            Console.WriteLine("response.MatchLevel = {0}", response.MatchLevel);
            Console.WriteLine("response.AddressType = {0}", response.AddressType);
            Console.WriteLine("response.AddressId = {0}", response.AddressId);
            Console.WriteLine("response.Postcode = {0}", response.Postcode);
            Console.WriteLine("response.PostalAddress = {0}", string.Join(",", response.PostalAddress));
        }
    }
}

// This code example produces the following output:
// response.Result = AddressAndEircodeMatch
// response.MatchLevel = AddressPoint
// response.AddressType = ResidentialAddressPoint
// response.AddressId = 1701984269
// response.Postcode = A86VC04
// response.PostalAddress = 8 SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH