using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using SOLA.WebApi.MemoryCaches;

namespace SOLA.WebApi.Filters
{
    public class HandleRequestFilter : IAutofacActionFilter
    {
        private readonly ICacheHelper cacheHelper;
        public HandleRequestFilter (ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            var customer = actionContext.Request.RequestUri.Host.Split('.')[0];

            cacheHelper.RequestScope.CustomerDataSource = cacheHelper.LifeTimeScope.CustomerDataSources[customer];
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }
}