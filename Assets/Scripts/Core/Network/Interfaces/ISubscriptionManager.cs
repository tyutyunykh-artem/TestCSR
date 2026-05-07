using System;

namespace Core.Network.Interfaces
{
    /// <summary>
    /// Интерфейс менеджера подписок клиентов на типы сообщений.
    /// </summary>
    public interface ISubscriptionManager : IDisposable
    {
        public IObservable<(int connectionId, string messageType)> OnClientSubscribed { get; }

        public void SubscribeClient(int connectionId, string messageType);
        public void UnsubscribeClient(int connectionId, string messageType);
        public void RemoveClient(int connectionId);
        public bool IsClientSubscribedTo(int connectionId, Type messageType);       
    }
}
