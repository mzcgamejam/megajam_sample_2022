using CommonProtocol;
using System.Threading.Tasks;

namespace GameServer
{
    public abstract class BaseController
    {
        public abstract Task<CBaseProtocol> DoPipeline(CBaseProtocol requestInfo);
    }
}
