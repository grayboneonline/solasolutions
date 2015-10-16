using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using SOLA.Cache;

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

    public class HandleRequestFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private readonly ICacheHelper cacheHelper;

        public HandleRequestFilterAttribute(ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var customer = actionContext.HttpContext.Request.Url.Host.Split('.')[0];

            cacheHelper.RequestScope.CustomerSite = customer;

            base.OnActionExecuting(actionContext);
        }
    }
}