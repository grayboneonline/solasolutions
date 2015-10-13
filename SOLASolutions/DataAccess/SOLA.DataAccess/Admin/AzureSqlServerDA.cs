using System.Collections.Generic;
using SOLA.DataAccess.Helpers;
using SOLA.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface IAzureSqlServerDA
    {
        IEnumerable<AzureSqlServer> GetAll();
    }

    public class AzureSqlServerDA : IAzureSqlServerDA
    {
        private readonly IAdminConfig config;

        public AzureSqlServerDA(IAdminConfig config)
        {
            this.config = config;
        }

        public IEnumerable<AzureSqlServer> GetAll()
        {
            return DataAccessHelper.Select<AzureSqlServer>(config);
        }
    }
}