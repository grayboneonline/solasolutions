using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using SOLA.MemoryCache;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public virtual void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //register types, modules ...
            builder.RegisterModule<MemoryCacheModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            HttpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(HttpConfig);
        }
    }
}