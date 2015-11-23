using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model
{
    /// <summary>
    /// Address Element
    /// </summary>
    public class AddressElement
    {
        [JsonConstructor]
        internal AddressElement(string value, AddressElementType type, int? addressId = null)
        {
            Value = value;
            Type = type;
            AddressId = addressId;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public AddressElementType Type { get; private set; }
        
        /// <summary>
        /// Gets the address id.
        /// </summary>
        public int? AddressId { get; private set; }
    }
}
