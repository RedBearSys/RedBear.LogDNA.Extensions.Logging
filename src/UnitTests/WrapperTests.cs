using RedBear.LogDNA.Extensions.Logging;
using Xunit;

namespace UnitTests
{
    public class WrapperTests
    {
        [Fact]
        public void ValueAssigned()
        {
            var wrapper = new Wrapper("foo");
            Assert.Equal("foo", wrapper.Value);
        }

        [Fact]
        public void ToStringPassedThrough()
        {
            var wrapper = new Wrapper("foo");
            Assert.Equal("foo", wrapper.ToString());
        }
    }
}
