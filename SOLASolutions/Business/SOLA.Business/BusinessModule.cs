using Autofac;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccessModule>();
        }
    }
}