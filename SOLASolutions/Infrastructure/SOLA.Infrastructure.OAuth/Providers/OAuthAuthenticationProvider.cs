using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace SOLA.Infrastructure.OAuth.Providers
{
    public class OAuthAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            var requestSite = context.Request.Uri.Host.Split('.')[0];
            var tokenSite = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeySite);
            if (tokenSite == null || requestSite != tokenSite.Value)
                return null;

            return base.ValidateIdentity(context);
        }
    }
}
