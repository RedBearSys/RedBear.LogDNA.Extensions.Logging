using System;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAProvider : ILoggerProvider
    {
        private readonly IApiClient _apiClient;
        private readonly LogDNAOptions _options;

        public LogDNAProvider(IApiClient apiClient, LogDNAOptions options)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Dispose()
        {
            if (_apiClient.Active)
            {
                _apiClient.Flush();
                _apiClient.Disconnect();
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogDNALogger(categoryName, _apiClient, _options);
        }
    }
}
