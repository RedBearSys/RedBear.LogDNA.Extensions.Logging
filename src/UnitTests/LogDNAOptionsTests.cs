using Microsoft.Extensions.Logging;
using RedBear.LogDNA.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNAOptionsTests
    {
        [Fact]
        public void AddNamespaceNullNamespaceThrowsException()
        {
            var options = new LogDNAOptions("key");

            Assert.Throws<ArgumentNullException>(() =>
            {
                options.AddNamespace(null, LogLevel.Debug);
            });
        }

        [Fact]
        public void AddNamespaceSuccessful()
        {
            var options = new LogDNAOptions("key")
                .AddNamespace("Foo", LogLevel.Warning);

            Assert.Contains(options.Namespaces, x => x.Namespace == "Foo" && x.LogLevel == LogLevel.Warning);
        }
    }
}
