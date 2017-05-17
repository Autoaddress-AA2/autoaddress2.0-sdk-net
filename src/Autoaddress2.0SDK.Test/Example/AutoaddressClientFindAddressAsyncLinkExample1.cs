using System;
using System.Linq;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.FindAddress;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientFindAddressAsyncLinkExample1
    {
        public static async void MainAsync()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            
            var request = new Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);
            var response1 = await autoaddressClient.FindAddressAsync(request);
            
            Console.WriteLine("response1.Result = {0}", response1.Result);
            Console.WriteLine("response1.AddressId = {0}", response1.AddressId);
            Console.WriteLine("response1.PostalAddress = {0}", string.Join(",", response1.PostalAddress));
            Console.WriteLine("response1.Options[1].DisplayName = {0}", response1.Options[1].DisplayName);

            var nextLink = response1.Options[1].Links.OfType<Model.FindAddress.Link>().First();
            var response2 = await autoaddressClient.FindAddressAsync(nextLink);
            
            Console.WriteLine("response2.Result = {0}", response2.Result);
            Console.WriteLine("response2.AddressId = {0}", response2.AddressId);
            Console.WriteLine("response2.Postcode = {0}", response2.Postcode);
            Console.WriteLine("response2.PostalAddress = {0}", string.Join(",", response2.PostalAddress));
        }
    }
}

// This code example produces the following output:
// response1.Result = IncompleteAddressEntered
// response1.AddressId = 1200021757
// response1.PostalAddress = SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH
// response1.Options[1].DisplayName = 1 SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH
// response2.Result = PostcodeAppended
// response2.AddressId = 1701984262
// response2.Postcode = A86W210
// response2.PostalAddress = 1 SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH