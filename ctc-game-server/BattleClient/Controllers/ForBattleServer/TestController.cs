using BattleProtocol;
using BattleProtocol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers.ForBattleServer
{
    class TestController : BaseController
    {
        public override async Task DoPipeline(BaseProtocol protocol)
        {
            var prot = protocol as ProtoTest;

            Console.WriteLine("Number : " + prot.Num);
            

        }
    }
}
