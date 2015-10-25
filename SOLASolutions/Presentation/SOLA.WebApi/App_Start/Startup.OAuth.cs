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

        private readonly Func<string, string, bool> validateUserNameAndPasswordFunc = (username, password) => username == password;

        private readonly Func<RefreshToken, bool> addRefreshTokenFunc = refreshToken =>
        {
            Container.Resolve<ILifeTimeScopeCache>().RefreshTokens.Add(refreshToken.MapTo<Models.OAuth.RefreshToken>());
            return true;
        };

        private readonly Func<string, string> getRefreshTokenProtectedTicketFunc = hashedToken =>
        {
            var lifeTimeScopeCache = Container.Resolve<ILifeTimeScopeCache>();
            return lifeTimeScopeCache.RefreshTokens.ContainsKey(hashedToken) ? lifeTimeScopeCache.RefreshTokens[hashedToken].ProtectedTicket : null;
        };

        private readonly Action<string> removeRefreshTokenAction = hashedToken => Container.Resolve<ILifeTimeScopeCache>().RefreshTokens.Remove(hashedToken);

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
                        ValidateUserNameAndPassword = validateUserNameAndPasswordFunc
                    },
                    RefreshTokenProvider = new RefreshTokenProvider
                    {
                        AddRefreshTokenFunc = addRefreshTokenFunc,
                        GetRefreshTokenProtectedTicketFunc = getRefreshTokenProtectedTicketFunc,
                        RemoveRefreshTokenFunc = removeRefreshTokenAction
                    }
                });
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = allowClients,
                    Provider = new OAuthAuthenticationProvider(),
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(WebConfig.Issuer, TextEncodings.Base64Url.Decode(WebConfig.Base64SymetricKey))
                    }
                });
        }
    }
}