using BattleClient.Controllers;
using BattleClient.Controllers.ForBattleServer;
using BattleProtocol;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient
{
    public class Connector
    {
        private AsyncTcpSession _asyncTcpSession = new AsyncTcpSession();

        private static Connector _instance = null;
        public static Connector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Connector();
                }
                return _instance;
            }
        }

        public void Connect(string ip, int port)
        {
            if (!_asyncTcpSession.IsConnected)
            {
                _asyncTcpSession.Connected += AsyncTcpSession_Connected; ;
                _asyncTcpSession.DataReceived += AsyncTcpSession_DataReceived;
                _asyncTcpSession.Closed += AsyncTcpSession_Closed;
                _asyncTcpSession.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            }
        }

        public void Close()
        {
            try
            {
                _asyncTcpSession.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        private async void AsyncTcpSession_DataReceived(object sender, DataEventArgs e)
        {
            int offset = 0;
            var sizeHeader = sizeof(MessageType);
            var intSize = sizeof(int);
            while (offset < e.Length)
            {
                var byteArrayHeader = e.Data.CloneRange(offset, sizeHeader);
                var byteArrayBodyLength = e.Data.CloneRange(offset + sizeHeader, intSize);
                var bodyLength = BitConverter.ToInt32(byteArrayBodyLength, 0);
                var body = e.Data.CloneRange(offset + intSize + sizeHeader, bodyLength);

                offset += (sizeHeader + intSize + bodyLength);

                var msgType = (MessageType)BitConverter.ToInt32(byteArrayHeader, 0);

                var proto = ProtocolFactory.DeserializeProtocol(msgType, body);

                var controller = ControllerFactory.CreateController(msgType);
                await controller.DoPipeline(proto);
            }
        }

        private void AsyncTcpSession_Connected(object sender, EventArgs e)
        {
        }

        private void AsyncTcpSession_Closed(object sender, EventArgs e)
        {
        }

        public bool IsConnected()
        {
            return _asyncTcpSession.IsConnected;
        }

        public void Send(MessageType messageType, byte[] data)
        {
            var header = BitConverter.GetBytes((int)messageType);
            using (var stream = new MemoryStream(header.Length + sizeof(int) + data.Length))
            {
                stream.Write(header, 0, header.Length);
                stream.Write(BitConverter.GetBytes(data.Length), 0, sizeof(int));
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                _asyncTcpSession.Send(stream.GetBuffer(), 0, (int)stream.Length);
            }
        }
    }
}
