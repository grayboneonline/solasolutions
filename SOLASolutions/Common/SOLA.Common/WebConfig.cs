using SOLA.Common.Attributes;

namespace SOLA.Common
{
    public static class WebConfig
    {
        [Config("SOLA-TokenEndPoint")]
        public static string TokenEndPoint { get; set; }

        [Config("SOLA-Issuer")]
        public static string Issuer { get; set; }

        [Config("SOLA-Base64SymetricKey")]
        public static string Base64SymetricKey { get; set; }

        [Config("SOLA-AccessTokenExpireMinutes")]
        public static int AccessTokenExpireMinutes { get; set; }

        static WebConfig()
        {
            ConfigAttribute.ReadConfigForType(typeof(WebConfig));
        }
    }
}
