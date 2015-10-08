using System;

namespace SOLA.Common
{
    [Obsolete("will be moved to web.config")]
    public class OAuthConstants
    {
        public const string TokenEndPoint = "/api/token";
        public const string Issuer = "http://localhost";
        public const string Base64SymetricKey = "LS0tLS0tU2hvcE1AbkBnM20zbnRTeXN0M20tLS0tLS0=";
        public const int AccessTokenExpireMinutes = 3;
    }
}
