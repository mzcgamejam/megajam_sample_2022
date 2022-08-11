using CommonProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    public class WebLogin : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqAccountJoin;

            var res = new ResAccountJoin
            {
                MessageType = MessageType.Login,
                ResponseType = ResponseType.Success
            };

            using (var db = new GameDB.DBConnector())
            {
                var query = new StringBuilder();
                query.Append("SELECT userId FROM users WHERE nickname ='")
                    .Append(req.Name).Append("';");

                using (var cursor = await db.ExecuteReaderAsync(query.ToString()))
                {
                    if (cursor.Read())
                    {
                        res.UserId = (int)cursor["userId"];
                        return res;
                    }
                        
                }
            }
            res.ResponseType = ResponseType.Fail;
            return res;
        }
    }
}
