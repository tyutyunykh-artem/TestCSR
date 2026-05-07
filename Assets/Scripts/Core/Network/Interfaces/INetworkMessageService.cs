using Mirror;

namespace Core.Network.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отправки сетевых сообщений.
    /// </summary>
    public interface INetworkMessageService
    {
        public void SendToSubscribed<T>(T message) where T : struct, NetworkMessage;
        public bool SendToClient<T>(int connectionId, T message) where T : struct, NetworkMessage;
    }
}
