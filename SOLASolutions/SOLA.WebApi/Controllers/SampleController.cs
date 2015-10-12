using System.Web.Http;
using SOLA.Infrastructure.WebApi.Attributes;
using SOLA.Infrastructure.WebApi.Base;
using SOLA.WebApi.MemoryCaches;

namespace SOLA.WebApi.Controllers
{
    [SOLAAuthorize]
    public class SampleController : BaseApiController
    {
        private readonly ISOLACache solaCache;
        public SampleController(ISOLACache solaCache)
        {
            this.solaCache = solaCache;
        }

        public IHttpActionResult Get()
        {
            
            return Ok(solaCache.RefreshTokens.Values);
        }
    }
}
