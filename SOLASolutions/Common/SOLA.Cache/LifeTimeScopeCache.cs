using SOLA.Cache.Base;
using SOLA.Cache.CacheObjects;

namespace SOLA.Cache
{
    public interface ILifeTimeScopeCache : IMemoryCache
    {
        void Initialize();
        CustomerDataSourceCache CustomerDataSources { get; set; }
        ApplicationClientCache ApplicationClients { get; set; }
        RefreshTokenCache RefreshTokens { get; set; }
    }

    public class LifeTimeScopeCache : MemoryCache, ILifeTimeScopeCache
    {
        public void Initialize()
        {
            CustomerDataSources = new CustomerDataSourceCache();
            ApplicationClients = new ApplicationClientCache();
            RefreshTokens = new RefreshTokenCache();
        }

        public CustomerDataSourceCache CustomerDataSources
        {
            get { return Get<CustomerDataSourceCache>(); }
            set { Set<CustomerDataSourceCache>(value); }
        }

        public ApplicationClientCache ApplicationClients
        {
            get { return Get<ApplicationClientCache>(); }
            set { Set<ApplicationClientCache>(value); }
        }

        public RefreshTokenCache RefreshTokens
        {
            get { return Get<RefreshTokenCache>(); }
            set { Set<RefreshTokenCache>(value); }
        }
    }
}
