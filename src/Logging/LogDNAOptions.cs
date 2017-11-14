using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace RedBear.LogDNA.Extensions.Logging
{

    // ReSharper disable once InconsistentNaming
    public class LogDNAOptions
    {
        public LogDNAOptions(string ingestionKey)
        {
            IngestionKey = ingestionKey;
        }

        public LogDNAOptions(string ingestionKey, LogLevel logLevel)
        {
            IngestionKey = ingestionKey;
            LogLevel = logLevel;
        }

        public string IngestionKey { get; }
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public List<NamespaceLogLevel> Namespaces { get; } = new List<NamespaceLogLevel>();
        public string HostName { get; set; } = Environment.MachineName;
        public List<string> Tags { get; } = new List<string>();
        public IMessageDetailFactory MessageDetailFactory { get; set; } = new MessageDetailFactory();

        public LogDNAOptions AddNamespace(string nameSpace, LogLevel logLevel)
        {
            Namespaces.Add(new NamespaceLogLevel(
                nameSpace ?? throw new ArgumentNullException(nameof(nameSpace)), 
                logLevel));
            return this;
        }
    }
}
