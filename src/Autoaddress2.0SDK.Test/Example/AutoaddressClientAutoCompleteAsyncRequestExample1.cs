using System;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.AutoComplete;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientAutoCompleteAsyncRequestExample1
    {
        public static async void Main()
        {
            const string address = "Silver Birches, Dunboyne";
            var autoaddressClient = new AutoaddressClient();

            var request = new Request(address: address, language: Language.EN, country: Country.IE, limit: 20, isVanityMode: false, addressProfileName: null);
            var response = await autoaddressClient.AutoCompleteAsync(request);

            Console.WriteLine("response.TotalOptions = {0}", response.TotalOptions);
            Console.WriteLine("response.Options[0].DisplayName = {0}", response.Options[0].DisplayName);
        }
    }
}

// This code example produces the following output:
// response.TotalOptions = 1
// response.Options[0].DisplayName = SILVER BIRCHES, MILLFARM, DUNBOYNE, CO. MEATH