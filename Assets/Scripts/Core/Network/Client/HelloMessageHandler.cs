using Core.Network.Interfaces;
using Core.Network.Messages;
using Mirror;
using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace Core.Network.Client
{
    /// <summary>
    /// Обработчик HelloMessage на клиенте.
    /// </summary>
    public class HelloMessageHandler : IDisposable
    {
        [Inject] private readonly IClientSubscriptionService _clientSubscriptionService;

        private readonly CompositeDisposable _disposables = new();

        public void Initialize()
        {
            if (NetworkClient.isConnected)
            {
                SubscribeToHelloMessage();
                return;
            }

            Observable.FromEvent(
                handler => NetworkClient.OnConnectedEvent += handler,
                handler => NetworkClient.OnConnectedEvent -= handler
            )
            .Take(1)
            .Subscribe(_ =>
            {
                SubscribeToHelloMessage();
            })
            .AddTo(_disposables);
        }

        private void SubscribeToHelloMessage()
        {
            NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessageReceived);
            _clientSubscriptionService.Subscribe<HelloMessage>();
            Debug.Log("[HelloMessageHandler.SubscribeToHelloMessage] Subscribed to HelloMessage");
        }

        private void OnHelloMessageReceived(HelloMessage message)
        {
            Debug.Log($"[HelloMessageHandler.OnHelloMessageReceived] Received: {message.Text}");
        }

        public void Dispose()
        {
            if (NetworkClient.isConnected)
            {
                NetworkClient.UnregisterHandler<HelloMessage>();
                _clientSubscriptionService.Unsubscribe<HelloMessage>();
            }

            _disposables.Dispose();
        }
    }
}
