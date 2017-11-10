using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNALogger : ILogger
    {
        private readonly IApiClient _client;
        private readonly string _loggerName;
        private readonly LogLevel _logLevel;

        public LogDNALogger(IApiClient client, string loggerName, LogLevel logLevel)
        {
            _client = client;
            _loggerName = loggerName;
            _logLevel = logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {

                var lv = state as FormattedLogValues;
                var value = lv?.GetFirstValue();
                var message = state.ToString();
                var originalMessage = (string)lv.Last().Value;

                if (Regex.IsMatch(originalMessage, @"\{[0-9]+\}") && value != null)
                {
                    var parts = value.GetType().ToString().Split('.');
                    if (parts.Length == 2 && parts[0] == "System")
                        value = null;
                }

                if (value == null && exception == null)
                {
                    LogMessage(_loggerName, logLevel, message);
                }
                else if (value == null)
                {
                    LogMessage(_loggerName, logLevel, exception.ToString());
                }
                else
                {
                    if (value is string)
                    {
                        LogMessage(_loggerName, logLevel, string.Format(message, value));
                    }
                    else
                    {
                        LogMessage(_loggerName, logLevel, $"{message} {JsonConvert.SerializeObject(value)}");
                    }
                }
            }
        }

        private void LogMessage(string logName, LogLevel logLevel, string message)
        {
            _client.AddLine(new LogLine(logName, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {ConvertLevel(logLevel)} {message}"));
        }

        private string ConvertLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return "CRITICAL";
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Information:
                    return "INFO";
                case LogLevel.None:
                    return "INFO";
                case LogLevel.Trace:
                    return "TRACE";
                case LogLevel.Warning:
                    return "WARN";
                default:
                    return "DEBUG";
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _logLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new LogDNAScope(_client, _loggerName, state?.ToString() ?? "[unnamed]");
        }
    }
}
