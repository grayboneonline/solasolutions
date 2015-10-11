using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using SOLA.Business;
using SOLA.Infrastructure.MemoryCache;
using SOLA.WebApi.MemoryCaches;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public virtual void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //register types, modules ...
            builder.RegisterType<SOLACache>().As<ISOLACache>().InstancePerRequest();

            builder.RegisterModule<MemoryCacheModule>();
            builder.RegisterModule<BusinessModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            HttpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(HttpConfig);
        }
    }
}