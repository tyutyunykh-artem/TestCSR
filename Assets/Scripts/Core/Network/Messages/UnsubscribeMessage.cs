using Mirror;

namespace Core.Network.Messages
{
    /// <summary>
    /// Сообщение отписки.
    /// </summary>
    public struct UnsubscribeMessage : NetworkMessage
    {
        public string MessageType;
    }
}
