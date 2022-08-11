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
    public class WebTryMatching : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqTryMatching;

            var res = new ResTryMatching
            {
                ResponseType = ResponseType.Fail
            };

            var client = new AmazonGameLiftClient();
            var glGameSessionRes = await client.CreateGameSessionAsync(new CreateGameSessionRequest
            {
                FleetId = "fleet-0efa4f7e-1829-4985-a9d5-b2fada292ddd",
                MaximumPlayerSessionCount = 2                
            });

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

            var isGetSessionSuccess = true;
            CreatePlayerSessionResponse glPlayerSessionRes = null;
            for (int i=0; i < 10; i++)
            {
                try
                {
                    glPlayerSessionRes = await client.CreatePlayerSessionAsync(new CreatePlayerSessionRequest
                    {
                        GameSessionId = glGameSessionRes.GameSession.GameSessionId,
                        PlayerId = req.UserId.ToString(),
                        PlayerData = "{}"
                    });

                    if (glPlayerSessionRes != null)
                        isGetSessionSuccess = true;
                }
                catch (InvalidGameSessionStatusException e)
                {
                    isGetSessionSuccess = false;
                    continue;
                }

                if (isGetSessionSuccess)
                    break;
            }


            if (glPlayerSessionRes == null)
            {
                Console.WriteLine("glPlayerSessionRes is Null");
            }
            else
            {
                if (false == isGetSessionSuccess)
                {
                    Console.WriteLine("isGetSessionSuccess Fail");
                }
                else
                {
                    Console.WriteLine("IP:" + glPlayerSessionRes.PlayerSession.IpAddress);
                    Console.WriteLine("Port:" + glPlayerSessionRes.PlayerSession.Port);
                    Console.WriteLine("PlayerSessionId:" + glPlayerSessionRes.PlayerSession.PlayerSessionId);
                }
                

                res.ResponseType = ResponseType.Success;
            }

            res.MessageType = MessageType.TryMatching;
            res.IpAddress = glGameSessionRes.GameSession.IpAddress;
            res.GameSessionId = glGameSessionRes.GameSession.GameSessionId;
            res.Port = glGameSessionRes.GameSession.Port;
                      

            //using (var db = new GameDB.DBConnector())
            //{
            //    var query = new StringBuilder();
            //    query.Append("SELECT userId FROM users WHERE nickname ='")
            //        .Append(req.UserId).Append("';");

            //    using (var cursor = await db.ExecuteReaderAsync(query.ToString()))
            //    {
            //        if (cursor.Read())
            //        {
            //            res.UserId = (int)cursor["userId"];
            //            return res;
            //        }

            //    }
            //}
            return res;
        }
    }
}
