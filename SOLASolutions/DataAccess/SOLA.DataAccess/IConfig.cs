using SOLA.Cache;

namespace SOLA.DataAccess
{
    public interface IConfig
    {
        string ConnectionString { get; set; }
    }

    public interface ICustomerConfig : IConfig
    {
    }

    public class CustomerConfig : ICustomerConfig
    {
        private const string ConnectionStringTmpl = "Data Source={0};Initial Catalog={1};User Id={2};Password={3}";

        public CustomerConfig(ICacheHelper cacheHelper)
        {
            var dataSource = cacheHelper.RequestScope.CustomerDataSource;

            ConnectionString = string.Format(ConnectionStringTmpl, dataSource.ServerName, dataSource.SiteName,
                dataSource.UserName, dataSource.Password);
        }

        public string ConnectionString { get; set; }
    }
}