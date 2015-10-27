using Autofac;
using SOLA.Business.Admin;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminManagement>().As<IAdminManagement>().InstancePerRequest();

            builder.RegisterType<LocationManagement>().As<ILocationManagement>().InstancePerRequest();
            builder.RegisterType<PageManagement>().As<IPageManagement>().InstancePerRequest();
            builder.RegisterType<PermissionManagement>().As<IPermissionManagement>().InstancePerRequest();
            builder.RegisterType<RoleManagement>().As<IRoleManagement>().InstancePerRequest();
            builder.RegisterType<UserManagement>().As<IUserManagement>().InstancePerRequest();

            builder.RegisterModule<DataAccessModule>();
        }
    }
}