﻿using Autofac;
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
            Container.RunInRequestScope(requestScope =>
            {
                var cacheHelper = requestScope.Resolve<ICacheHelper>();
                var adminManagement = requestScope.Resolve<IAdminManagement>();

                cacheHelper.LifeTimeScope.Initialize();
                cacheHelper.LifeTimeScope.ApplicationClients.AddRange(adminManagement.GetAllApplicationClient());
                cacheHelper.LifeTimeScope.CustomerDataSources.AddRange(adminManagement.GetAllAzureSqlDataSources());
            });
        }
    }
}