using System;
using Autofac;
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
using SOLA.MemoryCache;
using SOLA.WebApi.TemporaryDatasource;

namespace SOLA.WebApi
{
    public partial class Startup
    {
        #region Function & Action

		private readonly Func<string, ClientInfo> getClientFunc = clientId =>
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            var client = cacheManager.Get<ApplicationClientCache>(CacheKey.ApiClients).Get(clientId);
            if (client == null)
                return null;

            return new ClientInfo
            {
                AllowedOrigin = client.AllowedOrigin,
                IsActive = client.IsActive,
                RefreshTokenLifeTime = client.RefreshTokenLifeTime
            };
        };

        private readonly Func<string, string, bool> validateUserNameAndPasswordFunc = (username, password) => username == password;

        private readonly Func<string, string, string, DateTime, DateTime, string, bool> addRefreshTokenFunc = 
            (token, clientId, subject, issued, expires, protectedTicket) =>
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            cacheManager.Get<RefreshTokenCache>(CacheKey.RefreshTokens).Add(new RefreshToken
            {
                ClientId = clientId,
                Subject = subject,
                Token = token,
                IssuedUtc = issued,
                ExpiresUtc = expires,
                ProtectedTicket = protectedTicket,
            });
            
            return true;
        };

        private readonly Func<string, string> getRefreshTokenProtectedTicketFunc = hashedToken =>
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            var token = cacheManager.Get<RefreshTokenCache>(CacheKey.RefreshTokens)[hashedToken];
            return token == null ? null : token.ProtectedTicket;
        };

        private readonly Action<string> removeRefreshTokenAction = hashedToken =>
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            cacheManager.Get<RefreshTokenCache>(CacheKey.RefreshTokens).Remove(hashedToken);
        };

        #endregion

        public void ConfigureOAuth(IAppBuilder app)
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            var allowClients = cacheManager.Get<ApplicationClientCache>(CacheKey.ApiClients).GetAllClientId();

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