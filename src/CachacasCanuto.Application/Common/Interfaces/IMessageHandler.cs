using CachacasCanuto.Core.Helpers;

namespace CachacasCanuto.Application.Common.Interfaces
{
    public interface IMessageHandler
    {
        List<ErrorMessage> Messages { get; }
        bool HasMessage { get; }
        void AddMessage(ErrorMessage errorMessage);
        void AddMessages(List<ErrorMessage> messages);
    }
}
