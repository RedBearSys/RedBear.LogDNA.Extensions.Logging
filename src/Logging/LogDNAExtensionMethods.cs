﻿using Microsoft.Extensions.Logging;
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
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            if (messageDetailFactory == null)
                messageDetailFactory = new MessageDetailFactory();
            
            var client = await SetUpClientAsync(ingestionKey, hostName, tags);
            factory.AddProvider(new LogDNAProvider(client, logLevel, messageDetailFactory, inclusionRegex));
            return factory;
        }

        public static ILoggerFactory AddLogDNA(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            return factory.AddLogDNAAsync(ingestionKey, logLevel, hostName, tags, messageDetailFactory, inclusionRegex).Result;
        }

        public static async Task<ILoggingBuilder> AddLogDNAAsync(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            if (messageDetailFactory == null)
                messageDetailFactory = new MessageDetailFactory();

            var client = await SetUpClientAsync(ingestionKey, hostName, tags);
            builder.AddProvider(new LogDNAProvider(client, logLevel, messageDetailFactory, inclusionRegex));
            return builder;
        }

        public static ILoggingBuilder AddLogDNA(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            return builder.AddLogDNAAsync(ingestionKey, logLevel, hostName, tags, messageDetailFactory, inclusionRegex).Result;
        }

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
