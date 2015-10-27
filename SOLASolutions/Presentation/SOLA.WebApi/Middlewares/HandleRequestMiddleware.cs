using System.Threading.Tasks;
using Microsoft.Owin;
using SOLA.Cache;

namespace SOLA.WebApi.Middlewares
{
    public class HandleRequestMiddleware : OwinMiddleware
    {
        private readonly IRequestScopeCache requestScopeCache;
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;

        public HandleRequestMiddleware(OwinMiddleware next, IRequestScopeCache requestScopeCache, ILifeTimeScopeCache lifeTimeScopeCache)
            : base(next)
        {
            this.requestScopeCache = requestScopeCache;
            this.lifeTimeScopeCache = lifeTimeScopeCache;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var customerSite = context.Request.Host.Value.Split('.')[0];
            requestScopeCache.CustomerSite = customerSite;
            requestScopeCache.CustomerDataSource = lifeTimeScopeCache.CustomerDataSources[customerSite];

            await Next.Invoke(context);
        }
    }
}