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
    public class WebSearchRoom : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqUserId;

            var res = new ResSearchRoom
            {
                ResponseType = ResponseType.Fail
            };
            var infos = ConfigReader.Instance.GetInfos<Infos>();

            var client = new GameLiftClient();
            var glGameSessionRes = await client.SearchGameSessionsAsync();

            foreach (var gameSession in glGameSessionRes)
            {
                res.Rooms.Add(new ResRoomInfo
                {
                    GameSessionId = gameSession.GameSessionId,
                    Title = gameSession.GameProperties[0].Value,
                    Ip = gameSession.IpAddress,
                    Port = gameSession.Port
                });
            }

            return res;
        }
    }
}
