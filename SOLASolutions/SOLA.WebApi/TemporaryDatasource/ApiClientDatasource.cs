using System.Collections.Generic;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class ApiClientDatasource
    {
        public static List<ApiClient> Data = new List<ApiClient>
        {
            new ApiClient{ Id = 1, ClientId = "webapp", Name = "AngularJs Single Page Web Application", IsActive = true, AllowedOrigin = "http://localhost", RefreshTokenLifeTime = 600 }
        };
    }

    public class ApiClient
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string AllowedOrigin { get; set; }

        public int RefreshTokenLifeTime { get; set; }
    }
}