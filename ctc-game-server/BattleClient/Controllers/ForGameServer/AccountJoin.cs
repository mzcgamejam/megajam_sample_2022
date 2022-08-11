using CommonProtocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers.ForGameServer
{
    public class AccountJoin : Controller, IController
    {
        //public async Task Request(BaseRequest request)
        //{
            //var req = request as ReqAccountJoin;

            //var response
            //    = await client.PostAsJsonAsync(Targets.WebServer + MessageType.AccountJoin,
            //    JsonConvert.SerializeObject(req));

            //var res = GetResponse<ResAccountJoin>(await response.Content.ReadAsStringAsync());
            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("Success");
            //}
        //}
    }
}
