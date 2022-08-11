using BattleProtocol;
using BattleProtocol.Entities;
using CommonType;
using MessagePack;
using System.Threading.Tasks;

namespace BattleClient.Game
{
    public class BattleEnter
    {
        private static BattleEnter _instance = null;
        public static BattleEnter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleEnter();
                }
                return _instance;
            }
        }

        public async Task Run()
        {
            //Connector.Instance.Connect(Targets.BattleServerIP, Targets.BattleServerPort);
            while (Connector.Instance.IsConnected() == false) ;
            Connector.Instance.Send(MessageType.BattleEnter,
                MessagePackSerializer.Serialize(new ProtoBattleEnter
                {
                    Msg = MessageType.BattleEnter,
                    UserId = 10
                }));
            Game.Instance.GameStatus = GameStatus.BattleSearching;
        }
    }
}
