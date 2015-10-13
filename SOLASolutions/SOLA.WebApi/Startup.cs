using Autofac;
using Owin;
using System.Web.Http;
using SOLA.Business.Admin;
using SOLA.WebApi.MemoryCaches;
using SOLA.WebApi.TemporaryDatasource;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static HttpConfiguration HttpConfig { get; private set; }

        public void Configuration(IAppBuilder app)
        {
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
            var cacheHelper = Container.Resolve<ICacheHelper>();
            var adminManagement = Container.Resolve<IAdminManagement>();

            cacheHelper.LifeTimeScope.Initialize();
            cacheHelper.LifeTimeScope.ApplicationClients.AddRange(ApplicationClientDatasource.Data);
            cacheHelper.LifeTimeScope.CustomerDataSources.AddRange(adminManagement.GetAllAzureSqlDataSources());
        }
    }
}