using System;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace SOLA.Infrastructure.OAuth.Formats
{
    public class JwtTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string issuer = string.Empty;
        private readonly string base64SymetricKey = string.Empty;
 
        public JwtTokenFormat(string issuer, string base64SymetricKey)
        {
            this.issuer = issuer;
            this.base64SymetricKey = base64SymetricKey;
        }
 
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var clientId = data.Properties.Dictionary.ContainsKey(OAuthDefaults.HeaderKeyClientId) ? data.Properties.Dictionary[OAuthDefaults.HeaderKeyClientId] : null;

            if (string.IsNullOrWhiteSpace(clientId)) 
                throw new InvalidOperationException("AuthenticationTicket.Properties does not include client_id");

            var keyByteArray = TextEncodings.Base64Url.Decode(base64SymetricKey);
 
            var signingKey = new HmacSigningCredentials(keyByteArray);
 
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(issuer, clientId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
 
            var handler = new JwtSecurityTokenHandler();
 
            var jwt = handler.WriteToken(token);
 
            return jwt;
        }
 
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
