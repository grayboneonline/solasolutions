using System.Web.Mvc;
using System.Web.Routing;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        public virtual void ConfigureMvc()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}