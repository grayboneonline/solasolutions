using System.Web.Http;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static void ConfigureWebApi()
        {
            // Web API configuration and services

            // Web API routes
            HttpConfig.MapHttpAttributeRoutes();

            HttpConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
