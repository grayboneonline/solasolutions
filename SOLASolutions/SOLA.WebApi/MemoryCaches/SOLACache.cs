using SOLA.Infrastructure.MemoryCache;
using SOLA.WebApi.MemoryCaches.CacheObjects;

namespace SOLA.WebApi.MemoryCaches
{
    public interface ISOLACache
    {
        void Initialize();
        RefreshTokenCache RefreshTokens { get; }
        ApplicationClientCache ApplicationClients { get; }
    }

    public class SOLACache : ISOLACache
    {
        private readonly ICacheManager cacheManager;

        public SOLACache(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public void Initialize()
        {
            cacheManager.Initialize();
            cacheManager.Set(CacheKey.ApplicationClients, new ApplicationClientCache());
            cacheManager.Set(CacheKey.RefreshTokens, new RefreshTokenCache());
        }

        public RefreshTokenCache RefreshTokens
        {
            get { return cacheManager.Get<RefreshTokenCache>(CacheKey.RefreshTokens); }
        }

        public ApplicationClientCache ApplicationClients
        {
            get { return cacheManager.Get<ApplicationClientCache>(CacheKey.ApplicationClients); }
        }
    }
}