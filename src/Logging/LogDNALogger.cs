using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace RedBear.LogDNA.Extensions.Logging
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNALogger : ILogger
    {
        private readonly IApiClient _client;
        private readonly LogDNAOptions _options;
        private readonly string _loggerName;

        private static readonly AsyncLocal<Stack<string>> Scopes = new AsyncLocal<Stack<string>>();
        private const int MaxMessageSize = 16384; // 16KB

        public LogDNALogger(string loggerName, IApiClient client, LogDNAOptions options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _loggerName = loggerName ?? "Unknown";
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

                if (exception != null)
                    PruneExceptionDepth(exception);

                LogMessage(_loggerName, logLevel, message, value ?? exception);
            }
        }

        private void LogMessage(string logName, LogLevel logLevel, string message, object value)
        {
            var valueAsString = JsonConvert.SerializeObject(value,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            var messageDetail = _options.MessageDetailFactory.Create();

            messageDetail.Message = message;
            messageDetail.Level = ConvertLevel(logLevel);
            messageDetail.Value = new JRaw(valueAsString);


            messageDetail.Scope = Scopes.Value?.Count > 0 ? Scopes.Value?.Peek() : messageDetail.Scope;

            int length;
            string logLine;
            var now = DateTime.UtcNow;

            do
            {
                logLine =
                    $"{now:yyyy-MM-dd HH:mm:ss} {JsonConvert.SerializeObject(messageDetail, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})}";

                length = Encoding.UTF8.GetByteCount(logLine);

                if (length > MaxMessageSize)
                {
                    valueAsString = valueAsString.Substring(0, valueAsString.Length - 2);

                    // Deliberately not a JRaw at this point as we're starting to trim
                    // bits of the JSON string to make things fit into the 16KB limit.
                    // i.e. it's not valid JSON now.
                    messageDetail.Value = valueAsString;
                }

            } while (length > MaxMessageSize && valueAsString.Length > 0);

            _client.AddLine(new LogLine(logName, logLine));
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
            var match = _options.Namespaces.Where(x => _loggerName.StartsWith(x.Namespace)).OrderByDescending(x => x.Namespace)
                .FirstOrDefault();

            if (match != null)
            {
                return logLevel >= match.LogLevel && logLevel != LogLevel.None;
            }

            return logLevel >= _options.LogLevel && logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new LogDNAScope(this, state?.ToString() ?? "[unnamed]");
        }

        public void PushScope(string scope)
        {
            if (Scopes.Value == null) Scopes.Value = new Stack<string>();
            Scopes.Value.Push(scope);
            this.LogDebug($"--BEGIN SCOPE: {scope}--");
        }

        public void PopScope()
        {
            var scope = Scopes.Value.Peek();
            this.LogDebug($"--END SCOPE: {scope}--");
            Scopes.Value.Pop();
        }

        private void PruneExceptionDepth(Exception ex)
        {
            var inner = ex;
            var depth = 1;

            do
            {
                depth++;
                inner = inner.InnerException;
            } while (inner != null && depth < _options.MaxInnerExceptionDepth);

            if (inner != null)
            {
                // Remove
                var field = typeof(Exception).GetField("_innerException", BindingFlags.Instance | BindingFlags.NonPublic);
                field?.SetValue(inner, null);
            }
        }
    }
}
