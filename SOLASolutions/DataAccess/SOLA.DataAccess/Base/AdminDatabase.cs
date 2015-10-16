using PetaPoco;
using SOLA.DataAccess.Admin;

namespace SOLA.DataAccess.Base
{
    public class AdminDatabase : Database
    {
        public AdminDatabase(IAdminConfig config) : base(config.ConnectionString, config.ProviderName)
        {
        }
    }
}
