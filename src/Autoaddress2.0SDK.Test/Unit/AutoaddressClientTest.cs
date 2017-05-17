using System;
using Xunit;

namespace Autoaddress.Autoaddress2_0.Test.Unit
{
    public class AutoaddressClientTest
    {
        [Fact]
        public void Constructor_NullLicenceKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressClient((string)null));
        }

        [Fact]
        public void Constructor_LicenceKeySuppliedAndNullAutoaddressConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressClient("LicenceKey", null));
        }
    }
}
