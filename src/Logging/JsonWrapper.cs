using Newtonsoft.Json;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class JsonWrapper : Wrapper
    {
        public JsonWrapper(object value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"{JsonConvert.SerializeObject(Value)} .";
        }
    }
}
