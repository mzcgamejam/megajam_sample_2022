using BattleProtocol;
using BattleProtocol.Entities;
using BattleServer.Game;
using BattleServer.Server;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer.Controller.Controllers
{
    class BattleAttack : BaseController
    {
        public override async Task DoPipeline(BattleSession session, BaseProtocol requestInfo)
        {
            var req = requestInfo as ProtoBattleAttack;
            RoomManager.BattleAttack(req.GameSessionId, req.PlayerType, req.AttackType);
        }
    }
}
