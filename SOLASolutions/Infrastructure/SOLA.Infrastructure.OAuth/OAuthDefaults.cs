namespace SOLA.Infrastructure.OAuth
{
    public class OAuthDefaults
    {
        public const string TokenFormat = "JWT";
        public const string HeaderKeyClientId = "client_id";
        public const string HeaderKeySub = "sub";
        public const string HeaderKeyUserName = "username";
        public const string HeaderKeyAllowedOrigin = "Access-Control-Allow-Origin";
        public const string OwinKeyAllowedOrigin = "as:clientAllowedOrigin";
        public const string OwinKeyRefreshTokenLifeTime = "as:clientRefreshTokenLifeTime";
    }
}
