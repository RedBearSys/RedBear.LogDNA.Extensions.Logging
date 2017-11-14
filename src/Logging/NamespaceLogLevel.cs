using Microsoft.Extensions.Logging;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class NamespaceLogLevel
    {
        public NamespaceLogLevel(string nameSpace, LogLevel logLevel)
        {
            Namespace = nameSpace;
            LogLevel = logLevel;
        }

        public string Namespace { get; }
        public LogLevel LogLevel { get; }
    }
}
