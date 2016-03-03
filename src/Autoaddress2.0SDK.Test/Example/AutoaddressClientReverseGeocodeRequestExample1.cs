using System;
using Autoaddress.Autoaddress2_0.Model;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientReverseGeocodeRequestExample1
    {
        public static void Main()
        {
            const double longitude = -6.271796;
            const double latitude = 53.343761;
            const double maxDistance = 100;
            var autoaddressClient = new AutoaddressClient();
            var request = new Autoaddress2_0.Model.ReverseGeocode.Request(latitude: latitude, longitude: longitude, maxDistance: maxDistance, language: Language.EN, country: Country.IE, vanityMode: true, addressProfileName: null);

            var response = autoaddressClient.ReverseGeocode(request);

            Console.WriteLine("response.Hits[0].AddressId = {0}", response.Hits[0].AddressId);
            Console.WriteLine("response.Hits[0].PostalAddress = {0}", string.Join(",", response.Hits[0].PostalAddress));
            Console.WriteLine("response.Hits[0].VanityAddress = {0}", string.Join(",", response.Hits[0].VanityAddress));
        }
    }
}

// This code example produces the following output:
// response.Hits[0].AddressId = 1401182204
// response.Hits[0].PostalAddress = INNS COURT,WINETAVERN STREET,DUBLIN 8
// response.Hits[0].VanityAddress = Inns Court,Winetavern Street,Dublin 8