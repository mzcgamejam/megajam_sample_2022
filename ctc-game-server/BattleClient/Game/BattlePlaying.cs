using BattleClient.View;
using BattleProtocol;
using BattleProtocol.Entities;
using CommonType;
using MessagePack;
using System;
using System.Threading.Tasks;

namespace BattleClient.Game
{
    public class BattlePlaying
    {
        private static BattlePlaying _instance = null;
        public static BattlePlaying Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattlePlaying();
                }
                return _instance;
            }
        }

        public async Task Run()
        {
            Game.Instance.GameStatus = GameStatus.BattleEnter;
            
            ViewManager.Instance.View();
            //Connector.Instance.Connect(Targets.BattleServerIP, Targets.BattleServerPort);
            while (Connector.Instance.IsConnected() == false) ;
            Connector.Instance.Send(MessageType.BattleEnter,
                MessagePackSerializer.Serialize(new ProtoBattleEnter
                {
                    Msg = MessageType.BattleEnter,
                    UserId = 10
                }));

            
            var awaiter =  GetInput();


            await awaiter;
        }

        private async Task GetInput()
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
                    await BattleEnter.Instance.Run();
                    break;
                case "q":
                    Game.Instance.GameStatus = GameStatus.LoginAndAccountJoin;
                    break;
            }
        }
    }
}
