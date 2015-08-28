using Autoaddress.Autoaddress2_0.Helpers;

namespace Autoaddress.Autoaddress2_0.Settings
{
    internal struct Licence
    {
        public static string Key
        {
            get { return AppSettingsHelper.GetSetting("AutoAddress.AutoAddress2_0.Settings.Licence.Key"); }
        }
    }
}
