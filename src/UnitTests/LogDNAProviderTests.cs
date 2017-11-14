using Microsoft.Extensions.Logging;
using Moq;
using RedBear.LogDNA;
using RedBear.LogDNA.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAProviderTests
    {
        [Fact]
        public void ConstructorNullClientThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNAProvider(null, new LogDNAOptions("key"));
            });
        }

        [Fact]
        public void ConstructorNullOptionsThrowsException()
        {
            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNAProvider(client, null);
            });
        }

        [Fact]
        public void DisposedCorrectly()
        {
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.Active).Returns(true);
            mockClient.Setup(x => x.Disconnect()).Verifiable();
            mockClient.Setup(x => x.Flush()).Verifiable();
            var client = mockClient.Object;

            using (var unused = new LogDNAProvider(client, new LogDNAOptions("key")))
            {
                // Do nothing
            }

            mockClient.Verify();
        }

        [Fact]
        public void LoggerCreated()
        {
            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            var provider = new LogDNAProvider(client, new LogDNAOptions("key"));
            var result = provider.CreateLogger("Foo");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ILogger>(result);
        }
    }
}
