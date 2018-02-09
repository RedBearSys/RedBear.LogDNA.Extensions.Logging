using RedBear.LogDNA.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [Fact]
        public void CustomPropertiesOnRoot()
        {
            var detail = new MessageDetail
            {
                Value = "Foo"
            };

            detail.AddOrUpdateProperty("CustomString", "value1");
            detail.AddOrUpdateProperty("CustomInt", 12);

            var js = JsonConvert.SerializeObject(detail);
            var json = JObject.Parse(js);

            Assert.Equal("value1", json["CustomString"].Value<string>());
            Assert.Equal(12, json["CustomInt"].Value<int>());

            var obj = JsonConvert.DeserializeObject<MessageDetail>(js);
            Assert.NotEqual(default(KeyValuePair<string, object>), obj.Properties.FirstOrDefault(x => x.Key == "CustomString"));
            Assert.NotEqual(default(KeyValuePair<string, object>), obj.Properties.FirstOrDefault(x => x.Key == "CustomInt"));
        }
    }
}
