using System;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.FindAddress;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientExample3
    {
        public static void Main()
        {
            const string apiBaseAddress = "http://aa2-demo.autoaddress.ie";
            const int requestTimeoutMilliseconds = 5000;
            const string licenceKey = "TheLicenceKey";
            const string address = "8 Silver Birches, Dunboyne";
            var autoaddressConfig = new AutoaddressConfig(apiBaseAddress, requestTimeoutMilliseconds);
            var autoaddressClient = new AutoaddressClient(licenceKey, autoaddressConfig);
            
            var request = new Request(address: address, language: Language.EN, country: Country.IE, limit: 20, isVanityMode: false, addressProfileName: null);
            var response = autoaddressClient.FindAddress(request);
            
            Console.WriteLine("response.Result = {0}", response.Result);
            Console.WriteLine("response.Postcode = {0}", response.Postcode);
            Console.WriteLine("response.PostalAddress = {0}", string.Join(",", response.PostalAddress));
        }
    }
}

// This code example produces the following output:
// response.Result = PostcodeAppended
// response.Postcode = A86VC04
// response.PostalAddress = 8 SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH