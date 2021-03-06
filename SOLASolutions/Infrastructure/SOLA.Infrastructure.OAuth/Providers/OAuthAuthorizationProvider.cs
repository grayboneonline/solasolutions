﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SOLA.Infrastructure.OAuth.Contracts;

namespace SOLA.Infrastructure.OAuth.Providers
{
    public class OAuthAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public Func<string, ClientInfo> GetClientFunc { get; set; }

        public Func<string, string, UserInfo> GetUserByUserNameAndPasswordFunc { get; set; }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            if (GetClientFunc != null)
            {
                var client = GetClientFunc(context.ClientId);

                if (client == null)
                {
                    context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system", context.ClientId));
                    return Task.FromResult<object>(null);
                }

                if (!client.IsActive)
                {
                    context.SetError("invalid_clientId", "Client is inactive.");
                    return Task.FromResult<object>(null);
                }

                context.OwinContext.Set(OAuthDefaults.OwinKeyAllowedOrigin, client.AllowedOrigin);
                context.OwinContext.Set(OAuthDefaults.OwinKeyRefreshTokenLifeTime, client.RefreshTokenLifeTime);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>(OAuthDefaults.OwinKeyAllowedOrigin) ?? "*";
            context.OwinContext.Response.Headers.Add(OAuthDefaults.HeaderKeyAllowedOrigin, new[] { allowedOrigin });

            var user = GetUserByUserNameAndPasswordFunc(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                return Task.FromResult<object>(null);
            }
            var identity = new ClaimsIdentity(OAuthDefaults.TokenFormat);

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(OAuthDefaults.ClaimKeySub, context.UserName));
            identity.AddClaim(new Claim(OAuthDefaults.ClaimKeySite, GetCustomerSite(context.Request.Uri.Host)));
            identity.AddClaim(new Claim(OAuthDefaults.ClaimKeyUserId, user.Id.ToString()));
            identity.AddClaim(new Claim(OAuthDefaults.ClaimKeySessionId, Guid.NewGuid().ToString()));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         OAuthDefaults.HeaderKeyClientId, context.ClientId ?? string.Empty
                    },
                    { 
                        OAuthDefaults.HeaderKeyUserName, context.UserName
                    },
                });
            
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary[OAuthDefaults.HeaderKeyClientId];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            var originalSite = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == OAuthDefaults.ClaimKeySite);
            var currentSite = GetCustomerSite(context.Request.Uri.Host);
            if (originalSite == null || originalSite.Value != currentSite)
            {
                context.SetError("invalid_site", "Refresh token is issued to a different site.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var newClaim = newIdentity.Claims.FirstOrDefault(c => c.Type == OAuthDefaults.ClaimKeySessionId);
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim(OAuthDefaults.ClaimKeySessionId, Guid.NewGuid().ToString()));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                if (property.Key.ToLower() != ".issued" && property.Key.ToLower() != ".expires")
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        private string GetCustomerSite(string url)
        {
            return url.Split('.')[0];
        }
    }
}
