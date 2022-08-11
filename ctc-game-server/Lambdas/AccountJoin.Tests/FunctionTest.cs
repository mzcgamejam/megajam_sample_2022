using System;
using Xunit;
using CommonProtocol;
using System.Net;
using Newtonsoft.Json;

namespace AccountJoin.Tests
{

    public class FunctionTest
    {
        [Fact]
        public void TestToAccountJoin()
        {
            var webClient = new WebClient();

            var req = new ReqAccountJoin
            {
                Name = "GoGo7",
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var responseBytes
                = webClient.UploadString(new Uri("https://nq4m28r2tf.execute-api.ap-northeast-2.amazonaws.com/test/") + "AccountJoin", "POST"
                , JsonConvert.SerializeObject(req));

            var res = JsonConvert.DeserializeObject<ResAccountJoin>(responseBytes);
            Assert.True(res.ResponseType == ResponseType.Success || res.ResponseType == ResponseType.DuplicateName);
        }
    }
}
