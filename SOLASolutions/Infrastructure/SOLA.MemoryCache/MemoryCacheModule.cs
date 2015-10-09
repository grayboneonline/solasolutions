using Autofac;

namespace SOLA.MemoryCache
{
    public class MemoryCacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheManager>().As<ICacheManager>().SingleInstance();
        }
    }
}