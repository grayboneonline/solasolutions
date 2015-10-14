using System;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using SOLA.Business;
using SOLA.Cache;
using SOLA.Infrastructure.MemoryCache;
using SOLA.Infrastructure.WebApi.Base;
using SOLA.WebApi.Filters;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public virtual void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //register types, modules ...
            builder.RegisterType<HandleRequestFilter>()
                .AsWebApiActionFilterFor<BaseApiController>()
                .InstancePerRequest();

            builder.RegisterModule<MemoryCacheModule>();
            builder.RegisterModule<CacheModule>();
            builder.RegisterModule<BusinessModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(HttpConfig);

            Container = builder.Build();
            HttpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(HttpConfig);
        }
    }

    public static class ContainerExtensions
    {
        public static T RunInRequestScope<T>(this IContainer container, Func<ILifetimeScope, T> func)
        {
            using (var requestScope = container.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                return func(requestScope);
            }
        }

        public static void RunInRequestScope(this IContainer container, Action<ILifetimeScope> action)
        {
            using (var requestScope = container.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                action(requestScope);
            }
        }
    }
}