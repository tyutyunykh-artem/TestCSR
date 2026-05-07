using Core.Network.Interfaces;
using Core.Network.Messages;
using Mirror;
using System;
using UniRx;
using VContainer;

namespace Core.Network.Hosting
{
    /// <summary>
    /// Хост для обработки сетевых сообщений на сервере.
    /// </summary>
    public class ServerMessageHost : IDisposable
    {
        [Inject] private readonly ISubscriptionManager _subscriptionManager;
        
        private readonly CompositeDisposable _disposables = new();

        public void Initialize()
        {
            if (!NetworkServer.active)
            {
                return;
            }

            NetworkServer.RegisterHandler<SubscribeMessage>(OnSubscribeMessage);
            NetworkServer.RegisterHandler<UnsubscribeMessage>(OnUnsubscribeMessage);

            Observable.FromEvent<NetworkConnectionToClient>(
                handler => NetworkServer.OnDisconnectedEvent += handler,
                handler => NetworkServer.OnDisconnectedEvent -= handler
            )
            .Subscribe(conn =>
            {
                _subscriptionManager.RemoveClient(conn.connectionId);
            })
            .AddTo(_disposables);
        }

        private void OnSubscribeMessage(NetworkConnectionToClient conn, SubscribeMessage msg)
        {
            _subscriptionManager.SubscribeClient(conn.connectionId, msg.MessageType);
        }

        private void OnUnsubscribeMessage(NetworkConnectionToClient conn, UnsubscribeMessage msg)
        {
            _subscriptionManager.UnsubscribeClient(conn.connectionId, msg.MessageType);
        }

        public void Dispose()
        {
            if (NetworkServer.active)
            {
                NetworkServer.UnregisterHandler<SubscribeMessage>();
                NetworkServer.UnregisterHandler<UnsubscribeMessage>();
            }

            _disposables.Dispose();
        }
    }
}
