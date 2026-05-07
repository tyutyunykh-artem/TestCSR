using Core.Network.Interfaces;
using System;
using System.Collections.Generic;
using UniRx;

namespace Core.Network.Services
{
    /// <summary>
    /// Менеджер подписок клиентов на типы сообщений.
    /// </summary>
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly Dictionary<int, HashSet<string>> _subscriptions = new();
        private readonly Subject<(int connectionId, string messageType)> _onClientSubscribed = new();

        public IObservable<(int connectionId, string messageType)> OnClientSubscribed => _onClientSubscribed;

        public void SubscribeClient(int connectionId, string messageType)
        {
            if (!_subscriptions.TryGetValue(connectionId, out HashSet<string> clientSubscriptions))
            {
                clientSubscriptions = new HashSet<string>();
                _subscriptions[connectionId] = clientSubscriptions;
            }

            if (clientSubscriptions.Add(messageType))
            {
                _onClientSubscribed.OnNext((connectionId, messageType));
            }
        }

        public void UnsubscribeClient(int connectionId, string messageType)
        {
            if (_subscriptions.TryGetValue(connectionId, out HashSet<string> clientSubscriptions))
            {
                clientSubscriptions.Remove(messageType);
            }
        }

        public void RemoveClient(int connectionId)
        {
            _subscriptions.Remove(connectionId);
        }

        public bool IsClientSubscribedTo(int connectionId, Type messageType)
        {
            return _subscriptions.TryGetValue(connectionId, out HashSet<string> clientSubscriptions) && clientSubscriptions.Contains(messageType.FullName);
        }

        public void Dispose()
        {
            _onClientSubscribed?.Dispose();
            _subscriptions.Clear();
        }
    }
}
