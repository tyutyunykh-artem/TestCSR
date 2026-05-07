using Core.Network.Interfaces;
using Core.Network.Messages;
using Mirror;
using System;
using UniRx;
using VContainer;

namespace Core.Network.Demo
{
    /// <summary>
    /// Демо отправки HelloMessage подписанным клиентам.
    /// </summary>
    public class HelloMessageDemo : IDisposable
    {
        [Inject] private readonly ISubscriptionManager _subscriptionManager;
        [Inject] private readonly INetworkMessageService _networkMessageService;
        
        private readonly CompositeDisposable _disposables = new();

        public void Start()
        {
            if (!NetworkServer.active)
            {
                return;
            }

            _subscriptionManager.OnClientSubscribed
                .Where(eventData => eventData.messageType == typeof(HelloMessage).FullName)
                .Subscribe(eventData =>
                {
                    OnClientSubscribedToHello(eventData.connectionId);
                })
                .AddTo(_disposables);
        }

        private void OnClientSubscribedToHello(int connectionId)
        {
            HelloMessage message = new HelloMessage
            {
                Text = "Hello Client!"
            };

            _networkMessageService.SendToClient(connectionId, message);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
