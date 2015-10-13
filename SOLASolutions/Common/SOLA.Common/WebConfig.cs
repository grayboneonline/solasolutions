using SOLA.Common.Attributes;

namespace SOLA.Common
{
    public static class WebConfig
    {
        [Config("Auth-TokenEndPoint")]
        public static string TokenEndPoint { get; set; }

        [Config("Auth-Issuer")]
        public static string Issuer { get; set; }

        [Config("Auth-Base64SymetricKey")]
        public static string Base64SymetricKey { get; set; }

        [Config("Auth-AccessTokenExpireMinutes")]
        public static int AccessTokenExpireMinutes { get; set; }

        [Config("AdminConnectionString", ConfigType.ConnectionString)]
        public static string AdminConnectionString { get; set; }

        static WebConfig()
        {
            ConfigAttribute.ReadConfigForType(typeof(WebConfig));
        }
    }
}
