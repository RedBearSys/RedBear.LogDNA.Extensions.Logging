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
