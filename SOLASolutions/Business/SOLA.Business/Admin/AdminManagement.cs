using System.Collections.Generic;
using SOLA.DataAccess.Admin;
using SOLA.Business.Models.Admin;

namespace SOLA.Business.Admin
{
    public interface IAdminManagement
    {
        IEnumerable<CustomerDataSource> GetAllAzureSqlDataSources();
        IEnumerable<ApplicationClient> GetAllApplicationClient();
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
            return customerDA.GetAllAzureSqlDataSources().MapTo<IEnumerable<CustomerDataSource>>();
        }

        public IEnumerable<ApplicationClient> GetAllApplicationClient()
        {
            return new List<ApplicationClient>
                       {
                           new ApplicationClient
                               {
                                   Id = 1,
                                   ClientId = "webapp",
                                   Name = "AngularJs Single Page Web Application",
                                   IsActive = true,
                                   AllowedOrigin = "http://localhost",
                                   RefreshTokenLifeTime = 600
                               }
                       };
        }
    }
}