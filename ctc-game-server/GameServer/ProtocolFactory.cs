using CommonProtocol;
using MessagePack;
using System;
using System.IO;

namespace GameServer
{
    public static class ProtocolFactory
    {
        public static CBaseProtocol DeserializeProtocol(MessageType messageType, Stream protocol)
        {
            switch (messageType)
            {
                case MessageType.AccountJoin:
                case MessageType.Login:
                    return MessagePackSerializer.Deserialize<ReqAccountJoin>(protocol);
                case MessageType.TryMatching:
                    return MessagePackSerializer.Deserialize<ReqTryMatching>(protocol);
                case MessageType.CreateGame:
                    return MessagePackSerializer.Deserialize<ReqCreateGame>(protocol);
                case MessageType.SearchGame:
                case MessageType.EnterRoom:
                    return MessagePackSerializer.Deserialize<ReqUserId>(protocol);
                case MessageType.CreatePlayer:
                    return MessagePackSerializer.Deserialize<ReqCreatePlayer>(protocol);
                default:
                    throw new Exception("[ProtocolFactory] Invalid Message Type : " + messageType);
            }
        }

        public static byte[] SerializeProtocol(MessageType messageType, CBaseProtocol protocol)
        {
            switch (messageType)
            {
                case MessageType.AccountJoin:
                case MessageType.Login:
                    return MessagePackSerializer.Serialize((ResAccountJoin)protocol);
                case MessageType.TryMatching:
                    return MessagePackSerializer.Serialize((ResTryMatching)protocol);
                case MessageType.CreateGame:
                    return MessagePackSerializer.Serialize((ResCreateGame)protocol);
                case MessageType.CreatePlayer:
                    return MessagePackSerializer.Serialize((ResCreatePlayer)protocol);
                case MessageType.SearchGame:
                    return MessagePackSerializer.Serialize((ResSearchRoom)protocol);
                case MessageType.EnterRoom:
                    return MessagePackSerializer.Serialize((ResEnterRoom)protocol);
                default:
                    throw new Exception("[ProtocolFactory] Invalid Message Type : " + messageType);
            }
        }

        public static CBaseProtocol DeserializeProtocol(MessageType messageType, byte[] bytes)
        {
            switch (messageType)
            {
                case MessageType.AccountJoin:
                    return MessagePackSerializer.Deserialize<ReqAccountJoin>(bytes);
                default:
                    throw new Exception("[ProtocolFactory] Invalid Message Type : " + messageType);
            }
        }
    }
}
