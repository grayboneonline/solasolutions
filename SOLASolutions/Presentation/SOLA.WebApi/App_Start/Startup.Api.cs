using System.Web.Http;
using SOLA.Infrastructure.WebApi.Attributes;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static void ConfigureWebApi()
        {
            // Web API configuration and services
            HttpConfig.Filters.Add(new SOLAAuthorize());

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
