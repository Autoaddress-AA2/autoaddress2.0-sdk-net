using System;
using NUnit.Framework;

namespace Autoaddress.Autoaddress2_0.Test.Unit
{
    [TestFixture]
    public class AutoaddressConfigTest
    {
        [Test]
        public void Constructor_NullApiBaseAddress_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressConfig(null));
        }
        
        [Test]
        public void Constructor_RequestTimeoutMillisecondsEqualsMinus500_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutoaddressConfig("https://api.autoaddress.ie", -500));
        }
    }
}
