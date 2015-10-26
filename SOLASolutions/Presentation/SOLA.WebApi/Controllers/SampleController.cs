using System.Web.Http;
using SOLA.Cache;
using SOLA.Infrastructure.WebApi.Base;

namespace SOLA.WebApi.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : BaseApiController
    {
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;
        public SampleController(ILifeTimeScopeCache lifeTimeScopeCache)
        {
            this.lifeTimeScopeCache = lifeTimeScopeCache;
        }

        //[AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok(lifeTimeScopeCache.UserSessions.Values);
        }
    }
}
