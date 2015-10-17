﻿using System.Web.Http;
using SOLA.Cache;
using SOLA.Infrastructure.WebApi.Attributes;
using SOLA.Infrastructure.WebApi.Base;

namespace SOLA.WebApi.Controllers
{
    [SOLAAuthorize]
    [RoutePrefix("api/sample")]
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
