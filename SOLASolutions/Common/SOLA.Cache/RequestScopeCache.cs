using SOLA.Cache.Base;
using SOLA.Cache.Contracts;

namespace SOLA.Cache
{
    public interface IRequestScopeCache : IMemoryCache
    {
        ICustomerDataSource CustomerDataSource { get; set; }
        string CustomerSite { get; set; }
    }

    public class RequestScopeCache : MemoryCache, IRequestScopeCache
    {
        public ICustomerDataSource CustomerDataSource
        {
            get { return Get<ICustomerDataSource>(); }
            set { Set<ICustomerDataSource>(value); }
        }

        public string CustomerSite
        {
            get { return Get<ICustomerSite, string>(); }
            set { Set<ICustomerSite>(value); }
        }
    }

    #region Temporary Types

    public interface ICustomerSite{}
    #endregion
}
