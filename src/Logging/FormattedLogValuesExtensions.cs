using System.Linq;
using System.Reflection;

namespace RedBear.LogDNA.Extensions.Logging
{
    public static class FormattedLogValuesExtensions
    {
        public static object GetFirstValue(this FormattedLogValues lv)
        {
            var field = lv.GetType().GetField("_values", BindingFlags.NonPublic | BindingFlags.Instance);
            var values = (object[])field.GetValue(lv);
            return values?.FirstOrDefault();
        }
    }
}
