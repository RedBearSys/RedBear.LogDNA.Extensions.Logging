using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        #region "ILoggerFactory"
        public static async Task<ILoggerFactory> AddLogDNAAsync(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return await factory.AddLogDNAAsync(options);
        }

        public static async Task<ILoggerFactory> AddLogDNAAsync(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return await factory.AddLogDNAAsync(options);
        }

        public static async Task<ILoggerFactory> AddLogDNAAsync(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            var client = await SetUpClientAsync(options.IngestionKey, options.HostName, options.Tags);
            factory.AddProvider(new LogDNAProvider(client, options));
            return factory;
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return factory.AddLogDNAAsync(options).Result;
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return factory.AddLogDNAAsync(options).Result;
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            return factory.AddLogDNAAsync(options).Result;
        }
        #endregion

        #region "ILoggingBuilder"
        public static async Task<ILoggingBuilder> AddLogDNAAsync(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return await builder.AddLogDNAAsync(options);
        }

        public static async Task<ILoggingBuilder> AddLogDNAAsync(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return await builder.AddLogDNAAsync(options);
        }

        public static async Task<ILoggingBuilder> AddLogDNAAsync(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            var client = await SetUpClientAsync(options.IngestionKey, options.HostName, options.Tags);
            builder.AddProvider(new LogDNAProvider(client, options));
            return builder;
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey);
            return builder.AddLogDNAAsync(options).Result;
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel);
            return builder.AddLogDNAAsync(options).Result;
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            return builder.AddLogDNAAsync(options).Result;
        }
        #endregion

        private static async Task<ApiClient> SetUpClientAsync(
            string ingestionKey,
            string hostName,
            IEnumerable<string> tags)
        {
            if (tags == null)
                tags = new List<string>();

            var config = new Config(ingestionKey) { Tags = tags };

            if (!string.IsNullOrEmpty(hostName))
                config.HostName = hostName;

            var client = new ApiClient();

            await client.ConnectAsync(config);

            return client;
        }
    }
}
