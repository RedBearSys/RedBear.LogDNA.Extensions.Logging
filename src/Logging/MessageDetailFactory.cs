using System;
using System.Collections.Generic;

namespace RedBear.LogDNA.Extensions.Logging
{
    public class MessageDetailFactory : IMessageDetailFactory
    {
        private readonly List<Action<MessageDetail>> _handlers = new List<Action<MessageDetail>>();

        public void RegisterHandler(Action<MessageDetail> handlerAction)
        {
            _handlers.Add(handlerAction);
        }

        public MessageDetail Create()
        {
            var detail = new MessageDetail();

            foreach (var handler in _handlers)
            {
                handler(detail);
            }

            return detail;
        }
    }
}
