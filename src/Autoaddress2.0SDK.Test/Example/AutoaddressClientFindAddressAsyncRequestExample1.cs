using System;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.FindAddress;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientFindAddressAsyncRequestExample1
    {
        public static async void MainAsync()
        {
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            
            var request = new Request(address: address, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);
            var response = await autoaddressClient.FindAddressAsync(request);

            Console.WriteLine("response.Result = {0}", response.Result);
            Console.WriteLine("response.AddressId = {0}", response.AddressId);
            Console.WriteLine("response.Postcode = {0}", response.Postcode);
            Console.WriteLine("response.PostalAddress = {0}", string.Join(",", response.PostalAddress));
        }
    }
}

// This code example produces the following output:
// response.Result = PostcodeAppended
// response.AddressId = 1701984269
// response.Postcode = A86VC04
// response.PostalAddress = 8 SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH