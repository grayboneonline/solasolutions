using SOLA.Cache.Contracts;

namespace SOLA.Business.Models.Admin
{
    public class CustomerDataSource : ICustomerDataSource
    {
        public string SiteName { get; set; }

        public string ServerName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
