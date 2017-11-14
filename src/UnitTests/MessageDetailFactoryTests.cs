using RedBear.LogDNA.Extensions.Logging;
using Xunit;

namespace UnitTests
{
    public class MessageDetailFactoryTests
    {
        [Fact]
        public void CreatesSuccessfully()
        {
            var factory = new MessageDetailFactory();
            var result = factory.Create();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<MessageDetail>(result);
        }
    }
}
