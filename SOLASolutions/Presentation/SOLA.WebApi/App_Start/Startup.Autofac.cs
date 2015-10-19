using System;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using SOLA.Business;
using SOLA.Cache;
using SOLA.Infrastructure.MemoryCache;
using SOLA.Infrastructure.WebApi.Base;
using SOLA.WebApi.Controllers;
using SOLA.WebApi.Filters;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public virtual void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Api filters
            builder.RegisterWebApiFilterProvider(HttpConfig);
            builder.RegisterType<HandleRequestFilter>()
                .AsWebApiActionFilterFor<BaseApiController>()
                .InstancePerRequest();

            // Mvc filters
            builder.RegisterFilterProvider();
            builder.RegisterType<HandleRequestFilterAttribute>()
                .AsActionFilterFor<HomeController>()
                .InstancePerRequest();

            // Modules
            builder.RegisterModule<MemoryCacheModule>();
            builder.RegisterModule<CacheModule>();
            builder.RegisterModule<BusinessModule>();

            // Api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Mvc controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Mvc view property injection
            builder.RegisterSource(new ViewRegistrationSource());

            Container = builder.Build();

            HttpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

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