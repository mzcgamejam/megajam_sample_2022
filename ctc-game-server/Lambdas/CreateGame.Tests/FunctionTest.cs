using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using CreateGame;
using System.Net;
using Newtonsoft.Json;
using CommonProtocol;

namespace CreateGame.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToCreateGame()
        {
            var webClient = new WebClient();

            var req = new ReqCreateGame
            {
                GameName = "aaaaa",
                UserId = 2222
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var responseBytes
                = webClient.UploadString(new Uri("https://nq4m28r2tf.execute-api.ap-northeast-2.amazonaws.com/test/") + "CreateGame", "POST"
                , JsonConvert.SerializeObject(req));

            var res = JsonConvert.DeserializeObject<ResCreateGame>(responseBytes);
            Assert.True(res.ResponseType == ResponseType.Success || res.ResponseType == ResponseType.DuplicateName);
        }
    }
}
