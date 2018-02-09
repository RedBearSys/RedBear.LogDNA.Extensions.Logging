using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class MessageDetail
    {
        private object _value;

        [JsonExtensionData]
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

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

        [JsonIgnore]
        public IEnumerable<KeyValuePair<string, object>> Properties
        {
            get => _properties;
            set
            {
               _properties.Clear();

                foreach (var kvp in value)
                {
                    _properties.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public void AddOrUpdateProperty(string key, object value)
        {
            if (_properties.ContainsKey(key))
            {
                _properties.Remove(key);
            }

            _properties.Add(key, value);
        }
    }
}
