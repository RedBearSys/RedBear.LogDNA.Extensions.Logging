using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAScope : IDisposable
    {
        private readonly LogDNALogger _logger;

        private readonly string _scopeName;
        //private readonly IApiClient _client;
        //private readonly string _loggerName;
        //private readonly string _scopeName;
        //private readonly LogLevel _logLevel;

        //public LogDNAScope(IApiClient client, string loggerName, string scopeName, LogLevel logLevel)
        //{
        //    _client = client;
        //    _loggerName = loggerName;
        //    _scopeName = scopeName;
        //    _logLevel = logLevel;

        //    if (_logLevel <= LogLevel.Debug)
        //        _client.AddLine(new LogLine(_loggerName, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} DEBUG --BEGIN SCOPE: {_scopeName}--"));
        //}
        //public void Dispose()
        //{
        //    if (_logLevel <= LogLevel.Debug)
        //        _client.AddLine(new LogLine(_loggerName, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} DEBUG --END SCOPE: {_scopeName}--"));
        //}

        public LogDNAScope(LogDNALogger logger, string scopeName)
        {
            _logger = logger;
            _scopeName = scopeName;
            _logger.LogDebug($"--BEGIN SCOPE: {_scopeName}--");
        }

        public void Dispose()
        {
            _logger.LogDebug($"--END SCOPE: {_scopeName}--");
        }
    }
}
