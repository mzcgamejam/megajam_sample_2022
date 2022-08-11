using BattleProtocol;
using BattleProtocol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers.ForBattleServer
{
    class BattleEnterResultController : BaseController
    {
        public override async Task DoPipeline(BaseProtocol protocol)
        {
            var prot = protocol as ProtoBattleEnterResult;

            if (prot.ResultType == CommonType.ResultType.Success )
            {
                Game.Game.Instance.GameStatus = CommonType.GameStatus.BattlePlaying;
            }
        }
    }
}
