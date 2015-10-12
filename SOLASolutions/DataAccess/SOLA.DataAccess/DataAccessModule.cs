using Autofac;
using SOLA.DataAccess.Contracts;

namespace SOLA.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserDA>().As<IUserDA>().InstancePerRequest();
        }
    }
}