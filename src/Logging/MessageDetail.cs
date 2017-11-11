using Newtonsoft.Json;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class MessageDetail
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }
    }
}
