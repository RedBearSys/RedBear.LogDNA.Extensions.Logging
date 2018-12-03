using Microsoft.Extensions.Logging;
using Moq;
using RedBear.LogDNA;
using RedBear.LogDNA.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace UnitTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNALoggerTests
    {
        [Fact]
        public void ConstructorNullClientThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNALogger("name", null, new LogDNAOptions("key"));
            });
        }

        [Fact]
        public void ConstructorNullOptionsThrowsException()
        {
            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNALogger("name", client, null);
            });
        }

        [Fact]
        public void LogSimpleType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.Null(detail.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(), 
                new FormattedLogValues("message", null),
                null,
                null
                );
        }

        [Fact]
        public void LogFormattedSimpleType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message 1", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.Null(detail.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message {0}", 1),
                null,
                null
            );
        }

        [Fact]
        public void LogComplexType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.NotNull(detail.Value);

                var value = ((JObject) detail.Value).ToObject<KeyValuePair<string, string>>();
                Assert.Equal("key", value.Key);
                Assert.Equal("value", value.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message", new KeyValuePair<string, string>("key", "value")),
                null,
                null
            );
        }

        [Fact]
        public void LogWrapper()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.NotNull(detail.Value);

                var value = ((JObject)detail.Value).ToObject<KeyValuePair<string, string>>();
                Assert.Equal("key", value.Key);
                Assert.Equal("value", value.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message", new Wrapper(new KeyValuePair<string, string>("key", "value"))),
                null,
                null
            );
        }

        [Fact]
        public void CheckEnabled()
        {
            // Arrange
            var options = new LogDNAOptions("key", LogLevel.Debug)
                .AddNamespace("Microsoft.Something", LogLevel.Warning)
                .AddNamespace("Newtonsoft.Json", LogLevel.Critical);

            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            var logger = new LogDNALogger("Foo", client, options);
            // Should log debug
            Assert.True(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));

            logger = new LogDNALogger("Microsoft.Something.Blah", client, options);
            // Shouldn't log debug
            Assert.False(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));

            logger = new LogDNALogger("Newtonsoft.Json", client, options);
            // Shouldn't log debug
            Assert.False(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));
        }

        [Fact]
        public void LogException()
        {
            var options = new LogDNAOptions("key", LogLevel.Trace)
            {
                MaxInnerExceptionDepth = 3
            };

            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                Assert.Contains("Level 17", line.Content);
                Assert.DoesNotContain("Level 16", line.Content);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            var ex = CreateTwentyLevelException();

            logger.LogError(ex, ex.Message);
        }

        Exception CreateTwentyLevelException(Exception inner = null, int exceptionCount = 0)
        {
            var result = new Exception($"Level {exceptionCount}", inner);

            exceptionCount++;

            if (exceptionCount < 20)
                result = CreateTwentyLevelException(result, exceptionCount);

            try
            {
                throw result;
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        private MessageDetail GetDetail(string content)
        {
            return JsonConvert.DeserializeObject<MessageDetail>(content.Substring(content.IndexOf("{", StringComparison.Ordinal)));
        }
    }
}
