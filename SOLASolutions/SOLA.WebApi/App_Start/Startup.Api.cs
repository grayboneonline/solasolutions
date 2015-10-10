using System.Web.Http;
using SOLA.Infrastructure.WebApi.MessageHandlers;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public static void ConfigureWebApi()
        {
            // Web API configuration and services
            HttpConfig.MessageHandlers.Add(new RetrieveDataMessageHandler());

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
