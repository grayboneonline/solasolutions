using SOLA.Cache.Base;
using SOLA.Cache.CacheObjects;

namespace SOLA.Cache
{
    public interface ILifeTimeScopeCache : IMemoryCache
    {
        void Initialize();
        CustomerDataSourceCache CustomerDataSources { get; set; }
        ApplicationClientCache ApplicationClients { get; set; }
        UserSessionCache UserSessions { get; set; }
    }

    public class LifeTimeScopeCache : MemoryCache, ILifeTimeScopeCache
    {
        public void Initialize()
        {
            CustomerDataSources = new CustomerDataSourceCache();
            ApplicationClients = new ApplicationClientCache();
            UserSessions = new UserSessionCache();
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

        public UserSessionCache UserSessions
        {
            get { return Get<UserSessionCache>(); }
            set { Set<UserSessionCache>(value); }
        }
    }
}
