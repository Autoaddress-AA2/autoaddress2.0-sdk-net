using System;
using NUnit.Framework;

namespace Autoaddress.Autoaddress2_0.Test.Unit
{
    [TestFixture]
    public class AutoaddressClientTest
    {
        [Test]
        public void Constructor_NullLicenceKey_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressClient((string)null));
        }

        [Test]
        public void Constructor_NullAutoaddressConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressClient((AutoaddressConfig)null));
        }

        [Test]
        public void Constructor_LicenceKeySuppliedAndNullAutoaddressConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressClient("LicenceKey", null));
        }
    }
}
