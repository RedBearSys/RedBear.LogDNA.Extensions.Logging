using System;

namespace RedBear.LogDNA.Extensions.Logging
{
    public interface IMessageDetailFactory
    {
        void RegisterHandler(Action<MessageDetail> handlerAction);
        MessageDetail Create();
    }
}
