using Autofac;
using SOLA.Common;
using SOLA.DataAccess.Admin;
using SOLA.DataAccess.Base;

namespace SOLA.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerConfig>().As<ICustomerConfig>().InstancePerRequest();
            builder.Register(c => new AdminConfig { ConnectionString = WebConfig.AdminConnectionString, ProviderName = "System.Data.SqlClient" })
                   .As<IAdminConfig>()
                   .InstancePerRequest();

            builder.RegisterType<CustomerDatabase>().AsSelf().InstancePerRequest();
            builder.RegisterType<AdminDatabase>().AsSelf().InstancePerRequest();

            builder.RegisterType<AzureSqlServerDA>().As<IAzureSqlServerDA>().InstancePerRequest();
            builder.RegisterType<CustomerDA>().As<ICustomerDA>().InstancePerRequest();

            builder.RegisterType<LocationDA>().As<ILocationDA>().InstancePerRequest();
            builder.RegisterType<PageDA>().As<IPageDA>().InstancePerRequest();
            builder.RegisterType<PermissionDA>().As<IPermissionDA>().InstancePerRequest();
            builder.RegisterType<RoleDA>().As<IRoleDA>().InstancePerRequest();
            builder.RegisterType<UserDA>().As<IUserDA>().InstancePerRequest();
        }
    }
}