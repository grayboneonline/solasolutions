using Microsoft.Owin.Security.Infrastructure;
using SOLA.Infrastructure.OAuth.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SOLA.Infrastructure.OAuth.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public Action<Guid, string, int, RefreshToken> AddUserSessionFunc { get; set; }
        public Func<string, string> GetRefreshTokenProtectedTicketFunc { get; set; }
        public Action<string> RemoveRefreshTokenFunc { get; set; }
 
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary[OAuthDefaults.HeaderKeyClientId];

            var sessionidClaim = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeySessionId);
            var useridClaim = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeyUserId);

            if (!string.IsNullOrEmpty(clientid) && sessionidClaim != null && useridClaim != null && AddUserSessionFunc != null)
            {
                var refreshTokenId = Guid.NewGuid().ToString("n");
                var refreshTokenLifeTime = context.OwinContext.Get<int>(OAuthDefaults.OwinKeyRefreshTokenLifeTime);
                var issuedDate = DateTime.UtcNow;
                var expiresDate = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime);

                context.Ticket.Properties.IssuedUtc = issuedDate;
                context.Ticket.Properties.ExpiresUtc = expiresDate;

                var refreshToken = new RefreshToken
                {
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    Token = Helper.GetHash(refreshTokenId),
                    IssuedUtc = issuedDate,
                    ExpiresUtc = expiresDate,
                    ProtectedTicket = context.SerializeTicket(),
                };

                AddUserSessionFunc(Guid.Parse(sessionidClaim.Value), context.Request.Headers["User-Agent"], int.Parse(useridClaim.Value), refreshToken);
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
