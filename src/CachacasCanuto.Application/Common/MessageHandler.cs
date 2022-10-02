using CachacasCanuto.Application.Common.Interfaces;
using CachacasCanuto.Core.Helpers;

namespace CachacasCanuto.Application.Common
{
    public class MessageHandler : IMessageHandler
    {
        private readonly List<ErrorMessage> _messages;

        public MessageHandler()
        {
            _messages = new();
        }

        public List<ErrorMessage> Messages => _messages;

        public bool HasMessage => _messages.Any();

        public void AddMessage(ErrorMessage message) => _messages.Add(message);

        public void AddMessages(List<ErrorMessage> messages) => _messages.AddRange(messages);

    }
}
