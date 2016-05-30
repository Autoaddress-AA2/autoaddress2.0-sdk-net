using System;
using Autoaddress.Autoaddress2_0.Model.GetGbPostcodeData;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientGetGbPostcodeDataRequestExample1
    {
        public static void Main()
        {
            const string postcode = "BT11 8QT";
            var autoaddressClient = new AutoaddressClient();
            var request = new Request(postcode);

            var response = autoaddressClient.GetGbPostcodeData(request);

            Console.WriteLine("response.Result = {0}", response.Result);
            Console.WriteLine("response.Postcode = {0}", response.Postcode);
            Console.WriteLine("response.SpatialInfo.Wgs84.Location.Longitude = {0}", response.SpatialInfo.Wgs84.Location.Longitude);
            Console.WriteLine("response.SpatialInfo.Wgs84.Location.Latitude = {0}", response.SpatialInfo.Wgs84.Location.Latitude);
        }
    }
}

// This code example produces the following output:
// response.Result = PostcodeValid
// response.Postcode = BT11 8QT
// response.SpatialInfo.Wgs84.Location.Longitude = -5.993876
// response.SpatialInfo.Wgs84.Location.Latitude = 54.590367
