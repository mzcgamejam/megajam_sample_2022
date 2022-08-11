using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BattleClient.Controllers.ForGameServer
{
    public class Controller
    {
        protected HttpClient client = new HttpClient();

        public Controller()
        {
            //client.BaseAddress = new Uri(Targets.WebServer);

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T GetResponse<T>(string res)
        {
            var replaced1 = res.Replace(@"\", "").Remove(0, 1);
            return JsonConvert.DeserializeObject<T>(replaced1.Remove(replaced1.Length - 1, 1));
        }
    }
}
