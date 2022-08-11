using BattleClient.Game;
using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.View
{
    public class ViewManager
    {
        private static ViewManager _instance = null;
        public static ViewManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewManager();
                }
                return _instance;
            }
        }

        public void View()
        {
            switch (Game.Game.Instance.GameStatus)
            {
                case GameStatus.LoginAndAccountJoin:
                    LoginAndAccountJoin();
                    break;
                case GameStatus.BattleEnter:
                    BattleEnter();
                    break;
                case GameStatus.BattlePlaying:
                    BattlePlaying();
                    break;
                case GameStatus.BattleSearching:
                    BattleSearching();
                    break;
                default:
                    Console.WriteLine("Check the GameStatuc");
                    break;
            }
        }

        public async Task Input()
        {
            switch (Game.Game.Instance.GameStatus)
            {
                case GameStatus.LoginAndAccountJoin:
                    await LoginAndAccountJoinInput();
                    break;
                case GameStatus.BattleEnter:
                    BattleEnterInput();
                    break;
                case GameStatus.BattlePlaying:
                    BattlePlayingInput();
                    break;
                default:
                    Console.WriteLine("Check the GameStatuc");
                    break;
            }
        }

        public void LoginAndAccountJoin()
        {
            Console.Clear();
            Console.WriteLine("<<Game>>");
            Console.WriteLine("1.로그인");
            Console.WriteLine("2.가입");
            Console.WriteLine("3.배틀입장");
            Console.WriteLine("q.종료");
            Console.Write(":");
        }

        public async Task LoginAndAccountJoinInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    await Login.Instance.Run();
                    break;
                case "2":
                    await AccountJoin.Instance.Run();
                    break;
                case "3":
                    Game.Game.Instance.GameStatus = GameStatus.BattleEnter;
                    //await Game.BattleEnter.Instance.Run();
                    break;
                case "q":
                    Game.Game.Instance.GameStatus = GameStatus.GameEnd;
                    break;
            }
        }

        public void BattleEnter()
        {
            Console.Clear();
            Console.WriteLine("배틀 입장 중...");
            Console.WriteLine("q.종료");
            Console.Write(":");
        }

        public void BattleEnterInput()
        {
        }

        public void BattlePlaying()
        {
            Console.Clear();
            Console.WriteLine("배틀 시작...");
        }

        public void BattlePlayingInput()
        {
        }


        public void BattleSearching()
        {
            Console.Clear();
            Console.WriteLine("배틀 찾는 중...");
        }

        public void BattleSearchingInfo()
        {
        }
    }
}
