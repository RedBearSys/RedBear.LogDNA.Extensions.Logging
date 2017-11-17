using Newtonsoft.Json;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class MessageDetail
    {
        private object _value;

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueJson = value != null
                    ? $"{JsonConvert.SerializeObject(Value, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})} "
                    : null;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ValueJson { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Scope { get; set; }
    }
}
