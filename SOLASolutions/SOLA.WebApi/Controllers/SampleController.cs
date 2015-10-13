using System.Web.Http;
using SOLA.Infrastructure.WebApi.Attributes;
using SOLA.Infrastructure.WebApi.Base;
using SOLA.WebApi.MemoryCaches;

namespace SOLA.WebApi.Controllers
{
    public class SampleController : BaseApiController
    {
        private readonly ICacheHelper cacheHelper;
        public SampleController(ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        public IHttpActionResult Get()
        {
            
            return Ok(cacheHelper.LifeTimeScope.CustomerDataSources.Values);
        }
    }
}
