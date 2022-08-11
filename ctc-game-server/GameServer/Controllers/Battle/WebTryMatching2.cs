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
    public class WebTryMatching2 : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqTryMatching;

            var client = new AmazonGameLiftClient();
            var glRes = await client.StartGameSessionPlacementAsync(new StartGameSessionPlacementRequest
            {
                GameProperties = new List<GameProperty>
                {
                    new GameProperty{ Key = "", Value ="" }
                },
                GameSessionData = "",
                GameSessionName = "",
                GameSessionQueueName = "",
                MaximumPlayerSessionCount = 2,
                PlacementId = "",
                PlayerLatencies = new List<PlayerLatency>
                {
                    new PlayerLatency{ LatencyInMilliseconds = 20, PlayerId = Convert.ToString(req.UserId), RegionIdentifier = ""}
                },
                DesiredPlayerSessions = new List<DesiredPlayerSession>
                {
                    new DesiredPlayerSession { PlayerId = Convert.ToString(req.UserId), PlayerData = "" }
                }
            });

            

            var res = new ResAccountJoin
            {
                MessageType = MessageType.TryMatching,
                ResponseType = ResponseType.Success
            };

            using (var db = new GameDB.DBConnector())
            {
                var query = new StringBuilder();
                query.Append("SELECT userId FROM users WHERE nickname ='")
                    .Append(req.UserId).Append("';");

                using (var cursor = await db.ExecuteReaderAsync(query.ToString()))
                {
                    if (cursor.Read())
                    {
                        res.UserId = (int)cursor["userId"];
                        return res;
                    }

                }
            }
            res.ResponseType = ResponseType.Fail;
            return res;
        }
    }
}
