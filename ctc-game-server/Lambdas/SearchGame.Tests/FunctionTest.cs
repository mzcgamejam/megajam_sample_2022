using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using SearchGame;
using System.Net;
using CommonProtocol;
using Newtonsoft.Json;

namespace SearchGame.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToSearchGame()
        {
            var webClient = new WebClient();

            var req = new ReqUserId
            {
                UserId = 1
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var responseBytes
                = webClient.UploadString(new Uri("https://nq4m28r2tf.execute-api.ap-northeast-2.amazonaws.com/test/") + "SearchGame", "POST"
                , JsonConvert.SerializeObject(req));

            var res = JsonConvert.DeserializeObject<ResSearchRoom>(responseBytes);
            Assert.True(res.ResponseType == ResponseType.Success || res.ResponseType == ResponseType.DuplicateName || res.ResponseType == ResponseType.Fail);
        }
    }
}
