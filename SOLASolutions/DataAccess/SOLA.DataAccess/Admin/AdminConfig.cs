using SOLA.DataAccess.Base;

namespace SOLA.DataAccess.Admin
{
    public interface IAdminConfig : IConfig
    {
    }

    public class AdminConfig : IAdminConfig
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}