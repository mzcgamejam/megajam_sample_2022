using BattleClient.View;
using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Game
{
    public class Game
    {
        private static Game _instance = null;
        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Game();
                }
                return _instance;
            }
        }

        public GameStatus GameStatus = GameStatus.LoginAndAccountJoin;

        public async Task Run()
        {
            while (GameStatus != GameStatus.GameEnd)
            {
                ViewManager.Instance.View();
                await ViewManager.Instance.Input();
                ViewManager.Instance.View();
                await ModeRun();
            }
        }

        private async Task ModeRun()
        {
            switch (GameStatus)
            {
                case GameStatus.BattleEnter:
                    await BattleEnter.Instance.Run();
                    break;
            }
        }
    }
}
