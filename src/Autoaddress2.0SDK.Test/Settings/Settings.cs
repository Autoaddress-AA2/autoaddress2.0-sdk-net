using System;

namespace Autoaddress.Autoaddress2_0.Test.Settings
{
    internal struct Licence
    {
        private static string GetLicenceKey()
        {
            string licenceKey = Environment.GetEnvironmentVariable("Autoaddress2_0SDK_Test_LicenceKey");
            if (string.IsNullOrWhiteSpace(licenceKey))
            {
                throw new InvalidOperationException("Please create environment variable with name 'Autoaddress2_0SDK_Test_LicenceKey' and the value your developement licence key.");
            }

            return licenceKey;
        }

        public static string Key => GetLicenceKey();
    }
}