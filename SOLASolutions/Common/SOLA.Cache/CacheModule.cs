using Autofac;

namespace SOLA.Cache
{
    public class CacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheHelper>().As<ICacheHelper>().InstancePerRequest();
        }
    }
}
