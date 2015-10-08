using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System.Web.Http;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static HttpConfiguration HttpConfig { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfig = new HttpConfiguration();

            WebApiConfig.Register(HttpConfig);
            ConfigureAutofac(app);
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(HttpConfig);
        }
    }
}