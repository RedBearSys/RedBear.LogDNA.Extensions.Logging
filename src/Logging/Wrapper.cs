namespace RedBear.LogDNA.Extensions.Logging
{
    public class Wrapper
    {
        public Wrapper(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
}
