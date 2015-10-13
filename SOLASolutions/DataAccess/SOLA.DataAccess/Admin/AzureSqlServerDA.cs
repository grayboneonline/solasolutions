using System.Collections.Generic;
using SOLA.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface IAzureSqlServerDA
    {
        IEnumerable<AzureSqlServer> GetAll();
    }

    public class AzureSqlServerDA : BaseDA, IAzureSqlServerDA
    {

        public AzureSqlServerDA(IAdminConfig config) : base(config)
        {
        }

        public IEnumerable<AzureSqlServer> GetAll()
        {
            return Select<AzureSqlServer>();
        }
    }
}