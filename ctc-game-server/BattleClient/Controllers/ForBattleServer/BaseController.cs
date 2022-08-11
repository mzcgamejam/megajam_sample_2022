using BattleProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers.ForBattleServer
{
    public abstract class BaseController
    {        
        public abstract Task DoPipeline(BaseProtocol protocol);
    }
}
