using CommonProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    public class WebAccountJoin : BaseController
    {
        public override async Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo)
        {
            var req = requestInfo as ReqAccountJoin;

            var res = new ResAccountJoin
            {
                MessageType = MessageType.AccountJoin,
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
                        res.ResponseType = ResponseType.DuplicateName;
                        return res;
                    }
                }

                query.Clear();
                query.Append("INSERT INTO users (nickname, win, loss, draw, point) VALUES ('")
                    .Append(req.Name).Append("', 0, 0, 0, 0);");
                await db.ExecuteNonQueryAsync(query.ToString());
                res.UserId = (int)db.LastInsertedId();
            }

            return res;
        }
    }
}
