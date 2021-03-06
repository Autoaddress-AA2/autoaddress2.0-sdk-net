﻿using System;
using Autoaddress.Autoaddress2_0.Model;
using Autoaddress.Autoaddress2_0.Model.PostcodeLookup;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientPostcodeLookupRequestExample1
    {
        public static void Run()
        {
            const string postcode = "A86VC04";
            var autoaddressClient = new AutoaddressClient(Settings.Licence.Key);
            var request = new Request(postcode: postcode, language: Language.EN, country: Country.IE, limit: 20, geographicAddress: false, vanityMode: false, addressElements: false, addressProfileName: null);

            var response = autoaddressClient.PostcodeLookup(request);

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
// response.Result = ValidPostcode
// response.MatchLevel = AddressPoint
// response.AddressType = ResidentialAddressPoint
// response.AddressId = 1701984269
// response.Postcode = A86VC04
// response.PostalAddress = 8 SILVER BIRCHES,MILLFARM,DUNBOYNE,CO. MEATH