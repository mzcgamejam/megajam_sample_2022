using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using Login;
using System.Net;
using CommonProtocol;
using Newtonsoft.Json;

namespace Login.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToLogin()
        {

            var webClient = new WebClient();

            var req = new ReqAccountJoin
            {
                Name = "GoGo7",
            };
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var responseBytes
                = webClient.UploadString(new Uri("https://nq4m28r2tf.execute-api.ap-northeast-2.amazonaws.com/test/") + "Login", "POST"
                , JsonConvert.SerializeObject(req));

            var res = JsonConvert.DeserializeObject<ResAccountJoin>(responseBytes);
            Assert.True(res.ResponseType == ResponseType.Success || res.ResponseType == ResponseType.DuplicateName);
        }
    }
}
