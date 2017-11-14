using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RedBear.LogDNA;
using RedBear.LogDNA.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    // ReSharper disable once InconsistentNaming
    public class LogDNAScopeTests
    {
        [Fact]
        public async void TestScopes()
        {
            var scopes = new List<string>();
            var options = new LogDNAOptions("key", LogLevel.Trace);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                if (!string.IsNullOrEmpty(detail.Scope))
                    scopes.Add(detail.Scope);
            });

            var client = mockClient.Object;
            var logger = new LogDNALogger("name", client, options);

            logger.LogDebug("Starting up..");

            var tasks = new List<Task> { DoStuff1(logger), DoStuff2(logger) };
            await Task.WhenAll(tasks.ToArray());

            Assert.Equal(6, scopes.Count);
            Assert.Equal("DoStuff1", scopes[0]);
            Assert.Equal("DoStuff2", scopes[1]);
            Assert.Equal("DoStuff3", scopes[2]);
            Assert.Equal("DoStuff1", scopes[3]);
            Assert.Equal("DoStuff3", scopes[4]);
            Assert.Equal("DoStuff2", scopes[5]);

            logger.LogDebug("Finished!");
        }

        async Task DoStuff1(LogDNALogger logger)
        {
            using (var unused = logger.BeginScope("DoStuff1"))
            {
                await Task.Delay(500);
            }
        }

        async Task DoStuff2(LogDNALogger logger)
        {
            using (var unused = logger.BeginScope("DoStuff2"))
            {
                await DoStuff3(logger);
            }
        }

        async Task DoStuff3(LogDNALogger logger)
        {
            using (var unused = logger.BeginScope("DoStuff3"))
            {
                await Task.Delay(1500);
            }
        }

        private MessageDetail GetDetail(string content)
        {
            return JsonConvert.DeserializeObject<MessageDetail>(content.Substring(content.IndexOf("{", StringComparison.Ordinal)));
        }
    }
}
