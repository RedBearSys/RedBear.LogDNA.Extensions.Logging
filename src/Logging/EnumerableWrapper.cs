using System.Collections.Generic;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class EnumerableWrapper<T>
    {
        public EnumerableWrapper(IEnumerable<T> enumerable)
        {
            Values = enumerable;
        }

        public IEnumerable<T> Values { get; }
    }
}
