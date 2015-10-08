using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //register types, modules ...

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            HttpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(HttpConfig);
        }
    }
}