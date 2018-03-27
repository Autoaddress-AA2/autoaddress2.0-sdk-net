using Newtonsoft.Json;

namespace Autoaddress.Autoaddress2_0.Model.FindAddress
{
    /// <summary>
    /// Clean Result
    /// </summary>
    public class CleanResult
    {
        /// <summary>
        /// Clean Result constructor.
        /// </summary>
        [JsonConstructor]
        public CleanResult(bool isSpellingChanged, bool isFormatChanged, bool isAltered)
        {
            IsSpellingChanged = isSpellingChanged;
            IsFormatChanged = isFormatChanged;
            IsAltered = isAltered;
        }

        /// <summary>
        /// Gets whether or not spelling changed.
        /// </summary>
        public bool IsSpellingChanged { get; private set; }

        /// <summary>
        /// Gets whether or not format changed.
        /// </summary>
        public bool IsFormatChanged { get; private set; }

        /// <summary>
        /// Gets whether or not altered.
        /// </summary>
        public bool IsAltered { get; private set; }
    }
}
