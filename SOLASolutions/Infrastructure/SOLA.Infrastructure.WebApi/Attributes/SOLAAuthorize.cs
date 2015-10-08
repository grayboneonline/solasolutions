using System.Web.Http;
using System.Web.Http.Controllers;

namespace SOLA.Infrastructure.WebApi.Attributes
{
    public class SOLAAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}
