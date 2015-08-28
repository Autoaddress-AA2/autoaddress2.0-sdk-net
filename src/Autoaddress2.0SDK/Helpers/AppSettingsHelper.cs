using System;
using System.Linq;

namespace Autoaddress.Autoaddress2_0.Helpers
{
    internal static class AppSettingsHelper
    {
        public static string GetSetting(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }

        public static int GetSettingAsInteger(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var stringValue = GetSetting(name);

            int value;
            if (!int.TryParse(stringValue, out value))
            {
                throw new ArgumentException(string.Format("Property is not a number: {0}", name));
            }
            return value;
        }

        public static bool GetSettingAsBool(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var stringValue = GetSetting(name);

            if (string.Compare(stringValue.Trim(), "true", StringComparison.OrdinalIgnoreCase) == 0)
                return true;

            if (string.Compare(stringValue.Trim(), "false", StringComparison.OrdinalIgnoreCase) == 0)
                return false;

            throw new ArgumentException(string.Format("Property is not true/false: {0}", name));
        }

        public static string[] GetSettingAsList(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var stringValue = GetSetting(name);
            if (string.IsNullOrEmpty(stringValue))
                return new string[] { };

            return stringValue
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }
    }
}
