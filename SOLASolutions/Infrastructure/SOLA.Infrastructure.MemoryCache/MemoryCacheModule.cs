using Autofac;
using SOLA.Infrastructure.MemoryCache.LifeTimeScope;
using SOLA.Infrastructure.MemoryCache.RequestScope;

namespace SOLA.Infrastructure.MemoryCache
{
    public class MemoryCacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestScopeCache>().As<IRequestScopeCache>().InstancePerLifetimeScope();

            builder.RegisterType<LifeTimeScopeCache>().As<ILifeTimeScopeCache>().SingleInstance();
        }
    }
}