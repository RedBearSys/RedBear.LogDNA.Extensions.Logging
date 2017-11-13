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
        private readonly IMessageDetailFactory _messageDetailFactory;
        private readonly bool _ignored;

        public LogDNALogger(IApiClient client, string loggerName, LogLevel logLevel, IMessageDetailFactory messageDetailFactory, string inclusionRegex = "")
        {
            _client = client;
            _loggerName = loggerName;
            _logLevel = logLevel;
            _messageDetailFactory = messageDetailFactory;

            if (!string.IsNullOrEmpty(inclusionRegex))
            {
                _ignored = !Regex.IsMatch(loggerName, inclusionRegex);
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {

                var lv = state as FormattedLogValues;
                var value = lv?.GetFirstValue();
                var message = state.ToString();
                var originalMessage = (string)lv?.Last().Value ?? string.Empty;

                if (value is Wrapper w)
                    value = w.Value;
                
                if (Regex.IsMatch(originalMessage, @"\{[0-9]+\}") && value != null)
                {
                    var parts = value.GetType().ToString().Split('.');
                    if (parts.Length == 2 && parts[0] == "System")
                        value = null;
                }

                LogMessage(_loggerName, logLevel, message, value ?? exception);
            }
        }

        private void LogMessage(string logName, LogLevel logLevel, string message, object value)
        {
            var messageDetail = _messageDetailFactory.Create();

            messageDetail.Message = message;
            messageDetail.Level = ConvertLevel(logLevel);
            messageDetail.Value = value;

            _client.AddLine(new LogLine(logName,
                $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {JsonConvert.SerializeObject(messageDetail, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})}"));
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
            return !_ignored && logLevel >= _logLevel && logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new LogDNAScope(this, state?.ToString() ?? "[unnamed]");
        }
    }
}
