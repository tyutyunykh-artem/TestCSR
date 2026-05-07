using Mirror;

namespace Core.Network.Messages
{
    /// <summary>
    /// Тестовое сообщение.
    /// </summary>
    public struct HelloMessage : NetworkMessage
    {
        public string Text;
    }
}
