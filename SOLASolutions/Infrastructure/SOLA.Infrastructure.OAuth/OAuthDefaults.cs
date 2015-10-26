namespace SOLA.Infrastructure.OAuth
{
    public class OAuthDefaults
    {
        public const string TokenFormat = "JWT";
        public const string HeaderKeyClientId = "client_id";
        public const string HeaderKeyUserName = "username";
        public const string HeaderKeyAllowedOrigin = "Access-Control-Allow-Origin";
        public const string ClaimKeySub = "sub";
        public const string ClaimKeySite = "sit";
        public const string ClaimKeyUserId = "usr";
        public const string ClaimKeySessionId = "ses";
        public const string OwinKeyAllowedOrigin = "SOLA-clientAllowedOrigin";
        public const string OwinKeyRefreshTokenLifeTime = "SOLA-clientRefreshTokenLifeTime";
    }
}
