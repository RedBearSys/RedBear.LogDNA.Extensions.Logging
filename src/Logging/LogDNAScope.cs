using System;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAScope : IDisposable
    {
        private readonly IApiClient _client;
        private readonly string _loggerName;
        private readonly string _scopeName;

        public LogDNAScope(IApiClient client, string loggerName, string scopeName)
        {
            _client = client;
            _loggerName = loggerName;
            _scopeName = scopeName;
            _client.AddLine(new LogLine(_loggerName, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} INFO >> BEGIN SCOPE: {_scopeName} ..."));
        }
        public void Dispose()
        {
            _client.AddLine(new LogLine(_loggerName, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} INFO << END SCOPE: {_scopeName} ..."));
        }
    }
}
