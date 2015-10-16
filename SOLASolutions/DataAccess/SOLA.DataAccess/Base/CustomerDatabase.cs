using PetaPoco;

namespace SOLA.DataAccess.Base
{
    public class CustomerDatabase : Database
    {
        public CustomerDatabase(ICustomerConfig config) : base(config.ConnectionString, config.ProviderName)
        {
        }
    }
}
