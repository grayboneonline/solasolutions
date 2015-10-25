using Autofac;

namespace SOLA.Cache
{
    public class CacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestScopeCache>().As<IRequestScopeCache>().InstancePerRequest();

            builder.RegisterType<LifeTimeScopeCache>().As<ILifeTimeScopeCache>().SingleInstance();
        }
    }
}
