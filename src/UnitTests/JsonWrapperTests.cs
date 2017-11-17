using RedBear.LogDNA.Extensions.Logging;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class JsonWrapperTests
    {
        [Fact]
        public void ValueAssigned()
        {
            var wrapper = new JsonWrapper("foo");
            Assert.Equal("foo", wrapper.Value);
        }

        [Fact]
        public void ToStringPassedThrough()
        {
            var value = new KeyValuePair<string, string>("Foo", "Bar");
            var wrapper = new JsonWrapper(value);
            Assert.Equal("{\"Key\":\"Foo\",\"Value\":\"Bar\"} .", wrapper.ToString());
        }
    }
}
