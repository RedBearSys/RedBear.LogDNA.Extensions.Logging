using System;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAScope : IDisposable
    {
        private readonly LogDNALogger _logger;
        
        public LogDNAScope(LogDNALogger logger, string scopeName)
        {
            _logger = logger;
            _logger.PushScope(scopeName);
        }

        public void Dispose()
        {
            _logger.PopScope();
        }
    }
}
