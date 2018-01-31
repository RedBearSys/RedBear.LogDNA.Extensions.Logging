using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        #region "ILoggerFactory"
        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return factory.AddLogDNA(options);
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return factory.AddLogDNA(options);
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            var client = SetUpClient(options.IngestionKey, options.HostName, options.Tags, options.LogInternalsToConsole);
            factory.AddProvider(new LogDNAProvider(client, options));
            return factory;
        }
        #endregion

        #region "ILoggingBuilder"
        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return builder.AddLogDNA(options);
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return builder.AddLogDNA(options);
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            var client = SetUpClient(options.IngestionKey, options.HostName, options.Tags, options.LogInternalsToConsole);
            builder.AddProvider(new LogDNAProvider(client, options));
            return builder;
        }
        #endregion

        private static IApiClient SetUpClient(
            string ingestionKey,
            string hostName,
            IEnumerable<string> tags,
            bool logToConsole)
        {
            if (tags == null)
                tags = new List<string>();

            var config = new ConfigurationManager(ingestionKey) {Tags = tags, LogInternalsToConsole = logToConsole};

            if (!string.IsNullOrEmpty(hostName))
                config.HostName = hostName;

            var client = config.Initialise();
            client.Connect();

            return client;
        }
    }
}
