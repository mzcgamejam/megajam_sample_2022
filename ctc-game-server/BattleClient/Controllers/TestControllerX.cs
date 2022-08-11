using BattleClient.Controllers.ForBattleServer;
using BattleProtocol;
using BattleProtocol.Entities;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers
{
    public class TestControllerX
    {
        private static TestControllerX _instance = null;
        public static TestControllerX Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestControllerX();
                }
                return _instance;
            }
        }

        public void Send()
        {
            Connector.Instance.Connect("127.0.0.1", 50404);
            while (Connector.Instance.IsConnected() == false) ;
            Connector.Instance.Send(MessageType.Test,
                MessagePackSerializer.Serialize<ProtoTest>(new ProtoTest
                {
                    Msg = MessageType.Test,
                    Num = 10
                }));
        }
    }
}
