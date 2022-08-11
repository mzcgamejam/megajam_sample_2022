using CommonProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Game
{
    public class Login
    {

        private static Login _instance = null;
        public static Login Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Login();
                }
                return _instance;
            }
        }

        public async Task Run()
        {

            View();
            await GetInput();

        }

        private void View()
        {
            Console.Clear();
            Console.WriteLine("<<가입>>");
            Console.Write("Name:");
        }

        private async Task GetInput()
        {
            var name = Console.ReadLine();
            var request = new BattleClient.Controllers.ForGameServer.Login();
            //await request.Request(new ReqLogin
            //{
            //    Name = name
            //});
            //await request.Request(new ReqLogin
            //{
            //    Name = name
            //});
        }
    }
}
