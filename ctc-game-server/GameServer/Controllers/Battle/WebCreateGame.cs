using Amazon.GameLift;
using Amazon.GameLift.Model;
using CommonProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    public class WebCreateGame : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqCreateGame;

            var res = new ResCreateGame
            {
                ResponseType = ResponseType.Fail
            };

            var client = new GameLiftClient();

            var glGameSessionRes = await client.CreateGameSessionAsync(req.UserId, req.GameName);

            if (glGameSessionRes == null)
            {
                Console.WriteLine("glGameSessionRes is Null");
            }
            else
            {
                Console.WriteLine("IP:" + glGameSessionRes.GameSession.IpAddress);
                Console.WriteLine("Port:" + glGameSessionRes.GameSession.Port);
                Console.WriteLine("GameSessionId:" + glGameSessionRes.GameSession.GameSessionId);
            }

            res.ResponseType = ResponseType.Success;
            res.MessageType = MessageType.TryMatching;
            res.IpAddress = glGameSessionRes.GameSession.IpAddress;
            res.GameSessionId = glGameSessionRes.GameSession.GameSessionId;
            res.Port = glGameSessionRes.GameSession.Port;

            Console.WriteLine(res.GameSessionId);
            

            return res;
        }
    }
}
