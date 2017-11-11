namespace RedBear.LogDNA.Extensions.Logging
{
    public class MessageDetailFactory : IMessageDetailFactory
    {
        public MessageDetail Create()
        {
            return new MessageDetail();
        }
    }
}
