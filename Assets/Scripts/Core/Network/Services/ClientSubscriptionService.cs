using Core.Network.Interfaces;
using Core.Network.Messages;
using Mirror;
using System;

namespace Core.Network.Services
{
    /// <summary>
    /// Сервис для подписки клиента на сообщения.
    /// </summary>
    public class ClientSubscriptionService : IClientSubscriptionService
    {
        public void Subscribe<T>() where T : struct, NetworkMessage
        {
            if (!NetworkClient.isConnected)
            {
                throw new InvalidOperationException("Client is not connected");
            }

            SubscribeMessage subscribeMessage = new SubscribeMessage
            {
                MessageType = typeof(T).FullName
            };

            NetworkClient.Send(subscribeMessage);
        }

        public void Unsubscribe<T>() where T : struct, NetworkMessage
        {
            if (!NetworkClient.isConnected)
            {
                throw new InvalidOperationException("Client is not connected");
            }

            UnsubscribeMessage unsubscribeMessage = new UnsubscribeMessage
            {
                MessageType = typeof(T).FullName
            };

            NetworkClient.Send(unsubscribeMessage);
        }
    }
}
