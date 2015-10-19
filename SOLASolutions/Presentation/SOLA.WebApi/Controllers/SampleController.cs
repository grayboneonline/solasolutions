using System.Web.Http;
using SOLA.Cache;
using SOLA.Infrastructure.WebApi.Base;

namespace SOLA.WebApi.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : BaseApiController
    {
        private readonly ICacheHelper cacheHelper;
        public SampleController(ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            
            return Ok(cacheHelper.LifeTimeScope.CustomerDataSources.Values);
        }
    }
}
