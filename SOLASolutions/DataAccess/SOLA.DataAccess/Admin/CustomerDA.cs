using System.Collections.Generic;
using SOLA.DataAccess.Helpers;
using SOLA.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface ICustomerDA
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources();
    }

    public class CustomerDA : ICustomerDA
    {
        private readonly IAdminConfig config;

        public CustomerDA(IAdminConfig config)
        {
            this.config = config;
        }

        public IEnumerable<Customer> GetAll()
        {
            return DataAccessHelper.Select<Customer>(config);
        }

        public IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources()
        {
            var columns = new[]
            {
                "Customers.SiteName", "AzureSQLServers.ServerName", "AzureSQLServers.UserName", "AzureSQLServers.Password"
            };

            var query = "SELECT {0} " +
                        "FROM Customers INNER JOIN AzureSQLServers ON Customers.AzureSQLServerId = AzureSQLServers.AzureSQLServerId";

            return DataAccessHelper.Select<CustomerDataSource>(config, string.Format(query, string.Join(",", columns)));
        }
    }
}