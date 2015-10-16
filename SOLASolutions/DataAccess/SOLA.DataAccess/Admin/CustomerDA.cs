using System.Collections.Generic;
using SOLA.DataAccess.Base;
using SOLA.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface ICustomerDA : IBaseDA<Customer>
    {
        IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources();
    }

    public class CustomerDA : BaseDA<Customer>, ICustomerDA
    {
        public CustomerDA(AdminDatabase database) : base(database) { }

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