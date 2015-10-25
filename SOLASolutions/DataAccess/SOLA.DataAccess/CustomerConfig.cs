using SOLA.Cache;
using SOLA.DataAccess.Base;

namespace SOLA.DataAccess
{
    public interface ICustomerConfig : IConfig
    {
    }

    public class CustomerConfig : ICustomerConfig
    {
        private const string ConnectionStringTmpl = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        public CustomerConfig(IRequestScopeCache requestScopeCache)
        {
            var dataSource = requestScopeCache.CustomerDataSource;

            ConnectionString = string.Format(ConnectionStringTmpl, dataSource.ServerName, dataSource.SiteName,
                dataSource.UserName, dataSource.Password);
            ProviderName = "System.Data.SqlClient";
        }

        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}