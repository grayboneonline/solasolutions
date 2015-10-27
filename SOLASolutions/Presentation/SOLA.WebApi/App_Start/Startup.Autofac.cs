using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using SOLA.Business;
using SOLA.Cache;
using SOLA.WebApi.Middlewares;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public virtual void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // OWIN middlewares
            builder.RegisterType<HandleRequestMiddleware>().InstancePerRequest();

            // Modules
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
            app.UseAutofacMvc();
        }
    }
}