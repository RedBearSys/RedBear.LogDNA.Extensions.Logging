using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        public static async Task<ILoggerFactory> AddLogDNAAsync(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            return await factory.AddLogDNAAsync(ingestionKey, logLevel, new List<string>());
        }

        public static async Task<ILoggerFactory> AddLogDNAAsync(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel,
            IEnumerable<string> tags)
        {
            var config = new Config(ingestionKey) {Tags = tags};
            var client = new ApiClient();

            await client.ConnectAsync(config);

            factory.AddProvider(new LogDNAProvider(client, logLevel));

            return factory;
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            return factory.AddLogDNA(ingestionKey, logLevel, new List<string>());
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel,
            IEnumerable<string> tags)
        {
            var config = new Config(ingestionKey) { Tags = tags };
            var client = new ApiClient();

            client.ConnectAsync(config).Wait();

            factory.AddProvider(new LogDNAProvider(client, logLevel));

            return factory;
        }
    }
}
