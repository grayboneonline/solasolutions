using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace SOLA.Infrastructure.OAuth.Providers
{
    public class OAuthAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public Func<string, bool> ValidateSessionId { get; set; }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            var requestSite = context.Request.Uri.Host.Split('.')[0];
            var tokenSite = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeySite);
            if (tokenSite == null || requestSite != tokenSite.Value)
                return null;
            var sessionId = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeySessionId);
            if (sessionId == null || !ValidateSessionId(sessionId.Value))
                return null;

            return base.ValidateIdentity(context);
        }
    }
}
