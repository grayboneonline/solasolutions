using SOLA.Infrastructure.MemoryCache.LifeTimeScope;
using SOLA.Infrastructure.MemoryCache.RequestScope;
using SOLA.WebApi.MemoryCaches.CacheObjects;

namespace SOLA.WebApi.MemoryCaches
{
    public interface ICacheHelper
    {
        LifeTimeScope LifeTimeScope { get; }
        RequestScope RequestScope { get; }
    }

    public class CacheHelper : ICacheHelper
    {
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;
        private readonly IRequestScopeCache requestScopeCache;

        public CacheHelper(ILifeTimeScopeCache lifeTimeScopeCache, IRequestScopeCache requestScopeCache)
        {
            this.lifeTimeScopeCache = lifeTimeScopeCache;
            this.requestScopeCache = requestScopeCache;
        }

        public LifeTimeScope LifeTimeScope
        {
            get { return new LifeTimeScope(lifeTimeScopeCache); }
        }

        public RequestScope RequestScope
        {
            get { return new RequestScope(requestScopeCache); }
        }
    }

    public class LifeTimeScope
    {
        private readonly ILifeTimeScopeCache lifeTimeScopeCache;
        public LifeTimeScope(ILifeTimeScopeCache lifeTimeScopeCache)
        {
            this.lifeTimeScopeCache = lifeTimeScopeCache;
        }

        public void Initialize()
        {
            lifeTimeScopeCache.Initialize();
            lifeTimeScopeCache.Set(LifeTimeScopeCacheKey.ApplicationClients, new ApplicationClientCache());
            lifeTimeScopeCache.Set(LifeTimeScopeCacheKey.RefreshTokens, new RefreshTokenCache());
            lifeTimeScopeCache.Set(LifeTimeScopeCacheKey.CustomerDataSources, new CustomerCache());
        }

        public RefreshTokenCache RefreshTokens
        {
            get { return lifeTimeScopeCache.Get<RefreshTokenCache>(LifeTimeScopeCacheKey.RefreshTokens); }
        }

        public ApplicationClientCache ApplicationClients
        {
            get { return lifeTimeScopeCache.Get<ApplicationClientCache>(LifeTimeScopeCacheKey.ApplicationClients); }
        }

        public CustomerCache CustomerDataSources
        {
            get { return lifeTimeScopeCache.Get<CustomerCache>(LifeTimeScopeCacheKey.CustomerDataSources); }
        }
    }

    public class RequestScope
    {
        private readonly IRequestScopeCache requestScopeCache;
        public RequestScope(IRequestScopeCache requestScopeCache)
        {
            this.requestScopeCache = requestScopeCache;
        }

        public string CustomerSiteName
        {
            get { return requestScopeCache.Get<string>(RequestScopeCacheKey.Customer); }
            set { requestScopeCache.Set(RequestScopeCacheKey.Customer, value, true); }
        }
    }
}