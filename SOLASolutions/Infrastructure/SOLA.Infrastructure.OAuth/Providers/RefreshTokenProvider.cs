using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace SOLA.Infrastructure.OAuth.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public Func<string, string, string, DateTime, DateTime, string, bool> AddRefreshTokenFunc { get; set; }
        public Func<string, string> GetRefreshTokenProtectedTicketFunc { get; set; }
        public Action<string> RemoveRefreshTokenFunc { get; set; }
 
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary[OAuthDefaults.HeaderKeyClientId];

            if (!string.IsNullOrEmpty(clientid) && AddRefreshTokenFunc != null)
            {
                var refreshTokenId = Guid.NewGuid().ToString("n");
                var refreshTokenLifeTime = context.OwinContext.Get<int>(OAuthDefaults.OwinKeyRefreshTokenLifeTime);
                var issuedDate = DateTime.UtcNow;
                var expiresDate = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime);

                context.Ticket.Properties.IssuedUtc = issuedDate;
                context.Ticket.Properties.ExpiresUtc = expiresDate;

                var result = AddRefreshTokenFunc(Helper.GetHash(refreshTokenId), clientid, context.Ticket.Identity.Name, issuedDate, expiresDate, context.SerializeTicket());
                if(result)
                    context.SetToken(refreshTokenId);
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (GetRefreshTokenProtectedTicketFunc == null || RemoveRefreshTokenFunc == null)
                throw new ArgumentNullException();

            var allowedOrigin = context.OwinContext.Get<string>(OAuthDefaults.OwinKeyAllowedOrigin);
            context.OwinContext.Response.Headers.Add(OAuthDefaults.HeaderKeyAllowedOrigin, new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            var protectedTicket = GetRefreshTokenProtectedTicketFunc(hashedTokenId);
            if (protectedTicket != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(protectedTicket);
                RemoveRefreshTokenFunc(hashedTokenId);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
