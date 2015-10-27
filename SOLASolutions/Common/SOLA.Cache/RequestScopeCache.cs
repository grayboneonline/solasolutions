using System;
using SOLA.Cache.Base;
using SOLA.Cache.Contracts;

namespace SOLA.Cache
{
    public interface IRequestScopeCache : IMemoryCache
    {
        ICustomerDataSource CustomerDataSource { get; set; }
        string CustomerSite { get; set; }
        string Version { get; set; }
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

        private static string _version = null;

        public string Version
        {
            get
            {
                if (_version == null) _version = Guid.NewGuid().ToString().Replace("-", "");
                return _version;
            }

            set
            {
                Set<Version>(value);
            }
        }
    }

    #region Temporary Types

    public interface ICustomerSite{}
    #endregion
}
