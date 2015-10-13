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
                   .InstancePerLifetimeScope();

            builder.RegisterType<CustomerConfig>()
                   .As<ICustomerConfig>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AzureSqlServerDA>()
                   .As<IAzureSqlServerDA>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CustomerDA>()
                   .As<ICustomerDA>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserDA>()
                   .As<IUserDA>()
                   .InstancePerLifetimeScope();
        }
    }
}