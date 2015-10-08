using System.Web.Http;
using SOLA.Infrastructure.WebApi.Attributes;
using SOLA.Infrastructure.WebApi.Base;

namespace SOLA.WebApi.Controllers
{
    [SOLAAuthorize]
    public class SampleController : BaseApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("works");
        }

    }
}
