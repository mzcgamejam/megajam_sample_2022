using CommonProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Game
{
    public class AccountJoin
    {
        private static AccountJoin _instance = null;
        public static AccountJoin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountJoin();
                }
                return _instance;
            }
        }

        private bool _isPlaying = true;
        public async Task Run()
        {
            while (_isPlaying)
            {
                View();
                await GetInput();
            }
        }

        private void View()
        {
            Console.Clear();
            Console.WriteLine("<<가입>>");
            Console.Write("이름:");
        }

        private async Task GetInput()
        {

            //Console.Write("Where to move? [WASD] ");
            //ConsoleKeyInfo input;
            //while (!Console.KeyAvailable)
            //{
            //Move();
            //updateScreen();
            //}
            var name = Console.ReadLine();
            var request = new BattleClient.Controllers.ForGameServer.AccountJoin();
        }
    }
}
