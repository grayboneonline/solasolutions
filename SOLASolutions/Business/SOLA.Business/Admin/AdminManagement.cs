using System.Collections.Generic;
using SOLA.DataAccess.Admin;
using SOLA.Models.Admin;

namespace SOLA.Business.Admin
{
    public interface IAdminManagement
    {
        IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources();
    }

    public class AdminManagement : IAdminManagement
    {
        private readonly ICustomerDA customerDA;

        public AdminManagement(ICustomerDA customerDA)
        {
            this.customerDA = customerDA;
        }

        public IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources()
        {
            return customerDA.GetAllAzureSqlDataSources();
        }
    }
}