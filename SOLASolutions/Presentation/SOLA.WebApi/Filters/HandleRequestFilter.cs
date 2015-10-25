using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using SOLA.Cache;

namespace SOLA.WebApi.Filters
{
    public class HandleRequestFilter : IAutofacActionFilter
    {
        private readonly IRequestScopeCache requestScopeCache;
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;
        public HandleRequestFilter(IRequestScopeCache requestScopeCache, ILifeTimeScopeCache lifeTimeScopeCache)
        {
            this.requestScopeCache = requestScopeCache;
            this.lifeTimeScopeCache = lifeTimeScopeCache;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            var customer = actionContext.Request.RequestUri.Host.Split('.')[0];

            requestScopeCache.CustomerSite = customer;
            requestScopeCache.CustomerDataSource = lifeTimeScopeCache.CustomerDataSources[customer];
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }

    public class HandleRequestFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private readonly IRequestScopeCache requestScopeCache;
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;
        public HandleRequestFilterAttribute(IRequestScopeCache requestScopeCache, ILifeTimeScopeCache lifeTimeScopeCache)
        {
            this.requestScopeCache = requestScopeCache;
            this.lifeTimeScopeCache = lifeTimeScopeCache;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var customer = actionContext.HttpContext.Request.Url.Host.Split('.')[0];

            requestScopeCache.CustomerSite = customer;

            base.OnActionExecuting(actionContext);
        }
    }
}