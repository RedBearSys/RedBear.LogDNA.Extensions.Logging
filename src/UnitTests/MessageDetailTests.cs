using RedBear.LogDNA.Extensions.Logging;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class MessageDetailTests
    {
        [Fact]
        public void ValueJsonCorrect()
        {
            var value = new KeyValuePair<string, string>("Foo", "Bar");

            var detail = new MessageDetail
            {
                Value = value
            };

            Assert.Equal("{\"Key\":\"Foo\",\"Value\":\"Bar\"} ", detail.ValueJson);
        }

        [Fact]
        public void NullValueJsonCorrect()
        {
            var value = new KeyValuePair<string, string>("Foo", "Bar");

            var detail = new MessageDetail
            {
                Value = value
            };

            detail.Value = null;

            Assert.True(string.IsNullOrEmpty(detail.ValueJson));
        }
    }
}
