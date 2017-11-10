using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAProvider : ILoggerProvider
    {
        private readonly IApiClient _apiClient;
        private readonly LogLevel _logLevel;

        public LogDNAProvider(IApiClient apiClient, LogLevel logLevel)
        {
            _apiClient = apiClient;
            _logLevel = logLevel;
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
            return new LogDNALogger(_apiClient, categoryName, _logLevel);
        }
    }
}
