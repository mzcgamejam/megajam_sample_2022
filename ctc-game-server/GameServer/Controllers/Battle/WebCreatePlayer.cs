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
    public class WebCreatePlayer : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqCreatePlayer;
            var res = new ResCreatePlayer();
            res.ResponseType = ResponseType.Fail;

            var client = new GameLiftClient();

            var isGetSessionSuccess = true;
            CreatePlayerSessionResponse glPlayerSessionRes = null;
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    glPlayerSessionRes = await client.CreatePlayerSessionAsync(req.GameSessionId, req.UserId);
                    res.MessageType = MessageType.TryMatching;
                    res.IpAddress = glPlayerSessionRes.PlayerSession.IpAddress;
                    res.PlayerSessionId = glPlayerSessionRes.PlayerSession.PlayerSessionId;
                    res.Port = glPlayerSessionRes.PlayerSession.Port;

                    isGetSessionSuccess = true;

                }
                catch (InvalidGameSessionStatusException e)
                {
                    isGetSessionSuccess = false;
                    continue;
                }
                catch (GameSessionFullException e)
                {
                    isGetSessionSuccess = false;
                    continue;
                }
                catch (Exception e)
                {
                    isGetSessionSuccess = false;
                    continue;
                }

                if (isGetSessionSuccess)
                    break;
            }

            if (glPlayerSessionRes != null)
                res.ResponseType = ResponseType.Success;

            return res;

        }
    }
}
