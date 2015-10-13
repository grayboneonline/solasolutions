using SOLA.Infrastructure.MemoryCache.LifeTimeScope;
using SOLA.Infrastructure.MemoryCache.RequestScope;
using SOLA.Models.Admin;
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
            lifeTimeScopeCache.Set(LifeTimeScopeCacheKey.CustomerDataSources, new CustomerDataSourceCache());
        }

        public RefreshTokenCache RefreshTokens
        {
            get { return lifeTimeScopeCache.Get<RefreshTokenCache>(LifeTimeScopeCacheKey.RefreshTokens); }
        }

        public ApplicationClientCache ApplicationClients
        {
            get { return lifeTimeScopeCache.Get<ApplicationClientCache>(LifeTimeScopeCacheKey.ApplicationClients); }
        }

        public CustomerDataSourceCache CustomerDataSources
        {
            get { return lifeTimeScopeCache.Get<CustomerDataSourceCache>(LifeTimeScopeCacheKey.CustomerDataSources); }
        }
    }

    public class RequestScope
    {
        private readonly IRequestScopeCache requestScopeCache;
        public RequestScope(IRequestScopeCache requestScopeCache)
        {
            this.requestScopeCache = requestScopeCache;
        }

        public CustomerDataSource CustomerDataSource
        {
            get { return requestScopeCache.Get<CustomerDataSource>(RequestScopeCacheKey.CustomerDataSource); }
            set { requestScopeCache.Set(RequestScopeCacheKey.CustomerDataSource, value, true); }
        }
    }
}