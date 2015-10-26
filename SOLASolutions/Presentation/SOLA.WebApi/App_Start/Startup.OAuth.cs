using System;
using Autofac;
using SOLA.Cache;
using SOLA.Infrastructure.OAuth.Contracts;
using SOLA.Infrastructure.OAuth.Formats;
using SOLA.Infrastructure.OAuth.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SOLA.Common;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        #region Function & Action

        private readonly Func<string, ClientInfo> getClientFunc = clientId =>
        {
            var lifeTimeScopeCache = Container.Resolve<ILifeTimeScopeCache>();
            if (!lifeTimeScopeCache.ApplicationClients.ContainsKey(clientId))
                return null;

            var client = lifeTimeScopeCache.ApplicationClients[clientId];

            return new ClientInfo
            {
                AllowedOrigin = client.AllowedOrigin,
                IsActive = client.IsActive,
                RefreshTokenLifeTime = client.RefreshTokenLifeTime
            };
        };

        private readonly Func<string, string, UserInfo> getUserByUserNameAndPasswordFunc = (username, password) => username == password ? new UserInfo { Id = 1 } : null;

        private readonly Action<Guid, string, int, RefreshToken> addUserSessionFunc = (sessionId, userAgent, userId, refreshToken) =>
        {
            var userSession = new Models.OAuth.UserSession
                                  {
                                      Id = sessionId,
                                      UserAgent = userAgent,
                                      UserId = userId,
                                      UserName = refreshToken.Subject,
                                      RefreshToken = refreshToken.MapTo<Models.OAuth.RefreshToken>()
                                  };

            Container.Resolve<ILifeTimeScopeCache>().UserSessions.Add(userSession);
        };

        private readonly Func<string, string> getRefreshTokenProtectedTicketFunc = hashedToken =>
        {
            var lifeTimeScopeCache = Container.Resolve<ILifeTimeScopeCache>();
            var userSession = lifeTimeScopeCache.UserSessions.FindByHashedToken(hashedToken);
            return userSession == null ? null : userSession.RefreshToken.ProtectedTicket;
        };

        private readonly Action<string> removeRefreshTokenAction = hashedToken => Container.Resolve<ILifeTimeScopeCache>().UserSessions.RemoveByHashedToken(hashedToken);

        private readonly Func<string, bool> validateSessionId = sessionid =>
        {
            Guid id;
            if (!Guid.TryParse(sessionid, out id))
                return false;
            return Container.Resolve<ILifeTimeScopeCache>().UserSessions.ContainsKey(id);
        };

        #endregion

        public void ConfigureOAuth(IAppBuilder app)
        {
            var allowClients = Container.Resolve<ILifeTimeScopeCache>().ApplicationClients.GetAllClientId();

            app.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions
                {
                    //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString(WebConfig.TokenEndPoint),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(WebConfig.AccessTokenExpireMinutes),
                    AccessTokenFormat = new JwtTokenFormat(WebConfig.Issuer, WebConfig.Base64SymetricKey),
                    Provider = new OAuthAuthorizationProvider
                    {
                        GetClientFunc = getClientFunc,
                        GetUserByUserNameAndPasswordFunc = getUserByUserNameAndPasswordFunc
                    },
                    RefreshTokenProvider = new RefreshTokenProvider
                    {
                        AddUserSessionFunc = addUserSessionFunc,
                        GetRefreshTokenProtectedTicketFunc = getRefreshTokenProtectedTicketFunc,
                        RemoveRefreshTokenFunc = removeRefreshTokenAction
                    }
                });
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = allowClients,
                    Provider = new OAuthAuthenticationProvider
                                   {
                                       ValidateSessionId = validateSessionId
                                   },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(WebConfig.Issuer, TextEncodings.Base64Url.Decode(WebConfig.Base64SymetricKey))
                    }
                });
        }
    }
}