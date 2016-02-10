using System;
using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.ReverseGeocode
{
    /// <summary>
    /// Hit returned in ReverseGeocode Response object
    /// </summary>
    public class Hit
    {
        [JsonConstructor]
        internal Hit(int addressId, string[] postalAddress, string[] vanityAddress, string[] reformattedAddress, double distance)
        {
            if (postalAddress == null || postalAddress.Length == 0) throw new ArgumentNullException("postalAddress");
            if (distance < -0.00001D) throw new ArgumentNullException("distance");

            AddressId = addressId;
            PostalAddress = postalAddress;
            VanityAddress = vanityAddress;
            ReformattedAddress = reformattedAddress;
            Distance = distance;
        }

        public int AddressId { get; set; }
        public string[] PostalAddress { get; set; }
        public string[] VanityAddress { get; set; }
        public string[] ReformattedAddress { get; set; }
        public double Distance { get; set; }
    }
}