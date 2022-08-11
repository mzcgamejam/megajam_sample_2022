using System;
using Xunit;
using CommonProtocol;
using System.Net;
using Newtonsoft.Json;

namespace AccountJoin.Tests
{

    public class FunctionTestRemote
    {
        [Fact]
        public void TestToUpperFunction()
        {
            var webClient = new WebClient();

            var req = new ReqAccountJoin
            {
                Name = "GoGo7",
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var responseBytes
                = webClient.UploadString(new Uri("https://8apktb9np6.execute-api.ap-northeast-2.amazonaws.com/") + "AccountJoin", "POST"
                , JsonConvert.SerializeObject(req));

            var res = JsonConvert.DeserializeObject<ResAccountJoin>(responseBytes);
            Assert.True(res.ResponseType == ResponseType.Success || res.ResponseType == ResponseType.DuplicateName);
        }
    }
}
