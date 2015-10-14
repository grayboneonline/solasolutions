using Autofac;
using SOLA.Common;
using SOLA.DataAccess.Admin;

namespace SOLA.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AdminConfig { ConnectionString = WebConfig.AdminConnectionString })
                   .As<IAdminConfig>()
                   .InstancePerRequest();

            builder.RegisterType<CustomerConfig>()
                   .As<ICustomerConfig>()
                   .InstancePerRequest();

            builder.RegisterType<AzureSqlServerDA>()
                   .As<IAzureSqlServerDA>()
                   .InstancePerRequest();

            builder.RegisterType<CustomerDA>()
                   .As<ICustomerDA>()
                   .InstancePerRequest();

            builder.RegisterType<UserDA>()
                   .As<IUserDA>()
                   .InstancePerRequest();
        }
    }
}