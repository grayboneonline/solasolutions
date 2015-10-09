using System;
using System.Collections.Generic;
using System.Linq;
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
            var client = cacheManager.Get<List<ApiClient>>(CacheKey.ApiClients).FirstOrDefault(x => x.ClientId == clientId);
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
            RefreshTokenDatasource.Add(new RefreshToken
            {
                Id = RefreshTokenDatasource.GetNewId(),
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
            var token = RefreshTokenDatasource.GetByToken(hashedToken);
            return token == null ? null : token.ProtectedTicket;
        };

        private readonly Action<string> removeRefreshTokenAction = hashedToken => RefreshTokenDatasource.RemoveByToken(hashedToken);

        #endregion

        public void ConfigureOAuth(IAppBuilder app)
        {
            var cacheManager = Container.Resolve<ICacheManager>();
            var allowClients = cacheManager.Get<List<ApiClient>>(CacheKey.ApiClients).ConvertAll(x => x.ClientId);

            app.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions
                {
                    //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString(OAuthConstants.TokenEndPoint),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(OAuthConstants.AccessTokenExpireMinutes),
                    AccessTokenFormat = new JwtTokenFormat(OAuthConstants.Issuer, OAuthConstants.Base64SymetricKey),
                    Provider = new OAuthProvider
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
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(OAuthConstants.Issuer, TextEncodings.Base64Url.Decode(OAuthConstants.Base64SymetricKey))
                    }
                });
        }
    }
}