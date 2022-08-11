using Amazon.GameLift;
using Amazon.GameLift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    public class GameLiftClient
    {
        private AmazonGameLiftClient _client = null;
        private string _fleetId;
        private string _serviceURL;

        public GameLiftClient()
        {
            var infos = ConfigReader.Instance.GetInfos<Infos>();
            _fleetId = infos.GameLift.FleetId;
            _serviceURL = infos.GameLift.ServiceURL;
        }

        private AmazonGameLiftClient GetGameLiftClient()
        {
            if (_client != null)
                return _client;

            var infos = ConfigReader.Instance.GetInfos<Infos>();

            if (string.IsNullOrEmpty(infos.GameLift.ServiceURL))
            {
                _client = new AmazonGameLiftClient();
            }
            else
            {
                _client = new AmazonGameLiftClient(new AmazonGameLiftConfig
                {
                    ServiceURL = _serviceURL
                });
            }

            return _client;
        }

        public async Task<CreateGameSessionResponse> CreateGameSessionAsync(int userId, string gameName)
        {
            var client = GetGameLiftClient();
            return await client.CreateGameSessionAsync(new CreateGameSessionRequest
            {
                FleetId = _fleetId,
                MaximumPlayerSessionCount = 2,
                GameProperties = new List<GameProperty>
                {
                    new GameProperty { Key = "Title", Value = gameName.ToString()
                    }
                }
            });
        }

        public async Task<CreatePlayerSessionResponse> CreatePlayerSessionAsync(string gameSessionId, int userId)
        {
            var client = GetGameLiftClient();
            return await client.CreatePlayerSessionAsync(new CreatePlayerSessionRequest
            {
                GameSessionId = gameSessionId,
                PlayerId = userId.ToString(),
                PlayerData = "{}"
            });
        }

        //public async Task<SearchGameSessionsResponse> SearchGameSessionsAsync()
        public async Task<IEnumerable<GameSession>> SearchGameSessionsAsync()
        {
            var client = GetGameLiftClient();
            var gameSessions = await client.SearchGameSessionsAsync(new SearchGameSessionsRequest
            {
                FleetId = _fleetId,
                FilterExpression = "playerSessionCount = 1"

            });
            return gameSessions.GameSessions;
        }

        public async Task<GetGameSessionLogUrlResponse> GetGameSessionLogUrl(string gameSessionId)
        {
            var client = GetGameLiftClient();
            return await client.GetGameSessionLogUrlAsync(new GetGameSessionLogUrlRequest
            {
                GameSessionId = gameSessionId
            });
        }
    }
}
