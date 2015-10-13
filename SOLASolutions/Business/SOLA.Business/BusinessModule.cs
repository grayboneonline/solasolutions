using Autofac;
using SOLA.Business.Admin;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminManagement>().As<IAdminManagement>().InstancePerLifetimeScope();

            builder.RegisterModule<DataAccessModule>();
        }
    }
}