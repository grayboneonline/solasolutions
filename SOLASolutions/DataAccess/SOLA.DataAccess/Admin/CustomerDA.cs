using System.Collections.Generic;
using SOLA.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface ICustomerDA
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources();
    }

    public class CustomerDA : BaseDA, ICustomerDA
    {

        public CustomerDA(IAdminConfig config) : base(config)
        {
        }

        public IEnumerable<Customer> GetAll()
        {
            return Select<Customer>();
        }

        public IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources()
        {
            var columns = new[]
            {
                "Customers.SiteName", "AzureSQLServers.ServerName", "AzureSQLServers.UserName", "AzureSQLServers.Password"
            };

            var query = "SELECT {0} " +
                        "FROM Customers INNER JOIN AzureSQLServers ON Customers.AzureSQLServerId = AzureSQLServers.AzureSQLServerId";

            return Select<CustomerDataSource>(string.Format(query, string.Join(",", columns)));
        }
    }
}