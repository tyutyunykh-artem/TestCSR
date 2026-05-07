using Core.Network.Interfaces;
using Mirror;
using VContainer;

namespace Core.Network.Services
{
    /// <summary>
    /// Сервис отправки сетевых сообщений.
    /// </summary>
    public class NetworkMessageService : INetworkMessageService
    {
        [Inject] private readonly ISubscriptionManager _subscriptionManager;

        public void SendToSubscribed<T>(T message) where T : struct, NetworkMessage
        {
            if (!NetworkServer.active)
            {
                return;
            }

            foreach (int connectionId in NetworkServer.connections.Keys)
            {
                SendToClient(connectionId, message);
            }
        }

        public bool SendToClient<T>(int connectionId, T message) where T : struct, NetworkMessage
        {
            if (!NetworkServer.active)
            {
                return false;
            }

            if (!_subscriptionManager.IsClientSubscribedTo(connectionId, typeof(T)))
            {
                return false;
            }

            if (NetworkServer.connections.TryGetValue(connectionId, out NetworkConnectionToClient connection))
            {
                connection.Send(message);
                return true;
            }

            return false;
        }
    }
}
