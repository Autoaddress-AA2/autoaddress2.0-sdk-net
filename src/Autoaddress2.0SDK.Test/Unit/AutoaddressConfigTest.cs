using System;
using Xunit;

namespace Autoaddress.Autoaddress2_0.Test.Unit
{
    public class AutoaddressConfigTest
    {
        [Fact]
        public void Constructor_NullApiBaseAddress_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoaddressConfig(null));
        }
        
        [Fact]
        public void Constructor_RequestTimeoutMillisecondsEqualsMinus500_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutoaddressConfig("https://api.autoaddress.ie", -500));
        }
    }
}
