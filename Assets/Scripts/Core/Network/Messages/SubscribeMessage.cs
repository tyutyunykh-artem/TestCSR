using Mirror;

namespace Core.Network.Messages
{
    /// <summary>
    /// Сообщение подписки.
    /// </summary>
    public struct SubscribeMessage : NetworkMessage
    {
        public string MessageType;
    }
}
