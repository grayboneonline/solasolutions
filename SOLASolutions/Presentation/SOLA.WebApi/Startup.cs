using Autofac;
using Owin;
using System.Web.Http;
using SOLA.Business.Admin;
using SOLA.Cache;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static HttpConfiguration HttpConfig { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            WebApiMapper.Register();
            Business.BusinessMapper.Register();

            HttpConfig = new HttpConfiguration();

            ConfigureMvc();
            ConfigureWebApi();
            ConfigureAutofac(app);
           
            SetMemoryCache();

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(HttpConfig);
        }

        public void SetMemoryCache()
        {
            using (var requestScope = Container.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                var lifeTimeScopeCache = requestScope.Resolve<ILifeTimeScopeCache>();
                var adminManagement = requestScope.Resolve<IAdminManagement>();

                lifeTimeScopeCache.Initialize();
                lifeTimeScopeCache.ApplicationClients.AddRange(adminManagement.GetAllApplicationClient());
                lifeTimeScopeCache.CustomerDataSources.AddRange(adminManagement.GetAllAzureSqlDataSources());
            }
        }
    }
}