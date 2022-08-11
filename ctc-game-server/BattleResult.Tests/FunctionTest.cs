using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using BattleResult;
using Amazon.SQS;
using Amazon;
using Amazon.SQS.Model;
using CommonType;
using CommonProtocol;
using Newtonsoft.Json;

namespace BattleResult.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {
            var sqsConfig = new AmazonSQSConfig();
            sqsConfig.ServiceURL = "https://sqs.ap-northeast-2.amazonaws.com/831150897155/BattleResult-SQS";
            sqsConfig.RegionEndpoint = RegionEndpoint.APNortheast2;
            var sqsClient = new AmazonSQSClient(sqsConfig);

            var sqsRequest = new SendMessageRequest(
                "https://sqs.ap-northeast-2.amazonaws.com/831150897155/BattleResult-SQS",
                JsonConvert.SerializeObject(new SqsBattleResult
                {
                    UserId = 2,
                    WinType = WinType.Win
                }));

            await sqsClient.SendMessageAsync(sqsRequest);

            sqsRequest = new SendMessageRequest(
                "https://sqs.ap-northeast-2.amazonaws.com/831150897155/BattleResult-SQS",
                JsonConvert.SerializeObject(new SqsBattleResult
                {
                    UserId = 1,
                    WinType = WinType.Loss
                }));

            await sqsClient.SendMessageAsync(sqsRequest);

        }
    }
}
