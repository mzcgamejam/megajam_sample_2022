using CommonProtocol;
using GameServer.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    public static class ControllerFactory
    {
        public static BaseController CreateController(MessageType messageType, HttpContext context)
        {
            switch (messageType)
            {
                case MessageType.AccountJoin:
                    return new WebAccountJoin();
                case MessageType.Login:
                    return new WebLogin();
                case MessageType.TryMatching:
                    return new WebTryMatching();
                case MessageType.CreateGame:
                    return new WebCreateGame();
                case MessageType.CreatePlayer:
                    return new WebCreatePlayer();
                case MessageType.SearchGame:
                    return new WebSearchRoom();
                case MessageType.EnterRoom:
                default:
                    throw new Exception("[ControllerFactory] Invalid Message Type : " + messageType);
            }
        }
    }
}
