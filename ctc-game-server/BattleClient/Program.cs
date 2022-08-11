using BattleClient.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            await Game.Game.Instance.Run();
        }
    }
}
