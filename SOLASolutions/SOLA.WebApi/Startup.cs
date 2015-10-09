using Autofac;
using Owin;
using System.Web.Http;
using SOLA.MemoryCache;
using SOLA.WebApi.TemporaryDatasource;

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
           
            SetMemoryCache();

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(HttpConfig);
        }

        public void SetMemoryCache()
        {
            var cacheManager = Container.Resolve<ICacheManager>();

            cacheManager.Set(CacheKey.ApiClients, ApiClientDatasource.Data);
        }
    }
}