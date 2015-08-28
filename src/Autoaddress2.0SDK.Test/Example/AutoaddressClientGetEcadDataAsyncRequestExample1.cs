using System;
using Autoaddress.Autoaddress2_0.Model.GetEcadData;

namespace Autoaddress.Autoaddress2_0.Test.Example
{
    public class AutoaddressClientGetEcadDataAsyncRequestExample1
    {
        public static async void Main()
        {
            const int ecadId = 1200003223;
            var autoaddressClient = new AutoaddressClient();
            var request = new Request(ecadId);

            var response = await autoaddressClient.GetEcadDataAsync(request);

            Console.WriteLine("response.Result = {0}", response.Result);
            Console.WriteLine("response.EcadId = {0}", response.EcadId);
            Console.WriteLine("response.PostalAddress.English = {0}", string.Join(",", response.PostalAddress.English));
            Console.WriteLine("response.PostalAddress.Irish = {0}", string.Join(",", response.PostalAddress.Irish));
            Console.WriteLine("response.SpatialInfo.Etrs89.Location.Longitude = {0}", response.SpatialInfo.Etrs89.Location.Longitude);
            Console.WriteLine("response.SpatialInfo.Etrs89.Location.Latitude = {0}", response.SpatialInfo.Etrs89.Location.Latitude);
            Console.WriteLine("response.SpatialInfo.Etrs89.BoundingBox.Min.Longitude = {0}", response.SpatialInfo.Etrs89.BoundingBox.Min.Longitude);
            Console.WriteLine("response.SpatialInfo.Etrs89.BoundingBox.Min.Latitude = {0}", response.SpatialInfo.Etrs89.BoundingBox.Min.Latitude);
            Console.WriteLine("response.SpatialInfo.Etrs89.BoundingBox.Max.Longitude = {0}", response.SpatialInfo.Etrs89.BoundingBox.Max.Longitude);
            Console.WriteLine("response.SpatialInfo.Etrs89.BoundingBox.Max.Latitude = {0}", response.SpatialInfo.Etrs89.BoundingBox.Max.Latitude);
            Console.WriteLine("response.SpatialInfo.Ing.Location.Easting = {0}", response.SpatialInfo.Ing.Location.Easting);
            Console.WriteLine("response.SpatialInfo.Ing.Location.Northing = {0}", response.SpatialInfo.Ing.Location.Northing);
            Console.WriteLine("response.SpatialInfo.Ing.BoundingBox.Min.Easting = {0}", response.SpatialInfo.Ing.BoundingBox.Min.Easting);
            Console.WriteLine("response.SpatialInfo.Ing.BoundingBox.Min.Northing = {0}", response.SpatialInfo.Ing.BoundingBox.Min.Northing);
            Console.WriteLine("response.SpatialInfo.Ing.BoundingBox.Max.Easting = {0}", response.SpatialInfo.Ing.BoundingBox.Max.Easting);
            Console.WriteLine("response.SpatialInfo.Ing.BoundingBox.Max.Northing = {0}", response.SpatialInfo.Ing.BoundingBox.Max.Northing);
            Console.WriteLine("response.SpatialInfo.Itm.Location.Easting = {0}", response.SpatialInfo.Itm.Location.Easting);
            Console.WriteLine("response.SpatialInfo.Itm.Location.Northing = {0}", response.SpatialInfo.Itm.Location.Northing);
            Console.WriteLine("response.SpatialInfo.Itm.BoundingBox.Min.Easting = {0}", response.SpatialInfo.Itm.BoundingBox.Min.Easting);
            Console.WriteLine("response.SpatialInfo.Itm.BoundingBox.Min.Northing = {0}", response.SpatialInfo.Itm.BoundingBox.Min.Northing);
            Console.WriteLine("response.SpatialInfo.Itm.BoundingBox.Max.Easting = {0}", response.SpatialInfo.Itm.BoundingBox.Max.Easting);
            Console.WriteLine("response.SpatialInfo.Itm.BoundingBox.Max.Northing = {0}", response.SpatialInfo.Itm.BoundingBox.Max.Northing);
        }
    }
}

// This code example produces the following output:
// response.Result = EcadIdValid
// response.EcadId = 1200003223
// response.PostalAddress.English = DAME STREET,DUBLIN 2
// response.PostalAddress.Irish = SRÁID AN DÁMA,BAILE ÁTHA CLIATH 2
// response.SpatialInfo.Etrs89.Location.Longitude = -6.264906
// response.SpatialInfo.Etrs89.Location.Latitude = 53.344208
// response.SpatialInfo.Etrs89.BoundingBox.Min.Longitude = -6.267193
// response.SpatialInfo.Etrs89.BoundingBox.Min.Latitude = 53.343893
// response.SpatialInfo.Etrs89.BoundingBox.Max.Longitude = -6.262586
// response.SpatialInfo.Etrs89.BoundingBox.Max.Latitude = 53.344783
// response.SpatialInfo.Ing.Location.Easting = 315609.25
// response.SpatialInfo.Ing.Location.Northing = 234041.8
// response.SpatialInfo.Ing.BoundingBox.Min.Easting = 315457.88
// response.SpatialInfo.Ing.BoundingBox.Min.Northing = 234002.88
// response.SpatialInfo.Ing.BoundingBox.Max.Easting = 315764.14
// response.SpatialInfo.Ing.BoundingBox.Max.Northing = 234109
// response.SpatialInfo.Itm.Location.Easting = 715534.93
// response.SpatialInfo.Itm.Location.Northing = 734068.09
// response.SpatialInfo.Itm.BoundingBox.Min.Easting = 715383.6
// response.SpatialInfo.Itm.BoundingBox.Min.Northing = 734029.19
// response.SpatialInfo.Itm.BoundingBox.Max.Easting = 715689.78999
// response.SpatialInfo.Itm.BoundingBox.Max.Northing = 734135.3