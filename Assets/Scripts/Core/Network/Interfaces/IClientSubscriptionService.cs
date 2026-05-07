using Mirror;

namespace Core.Network.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для подписки клиента на сообщения.
    /// </summary>
    public interface IClientSubscriptionService
    {
        public void Subscribe<T>() where T : struct, NetworkMessage;
        public void Unsubscribe<T>() where T : struct, NetworkMessage;
    }
}
