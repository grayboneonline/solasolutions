using System.Collections.Generic;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class ApplicationClientDatasource
    {
        public static List<ApplicationClient> Data = new List<ApplicationClient>
        {
            new ApplicationClient{ Id = 1, ClientId = "webapp", Name = "AngularJs Single Page Web Application", IsActive = true, AllowedOrigin = "http://localhost", RefreshTokenLifeTime = 600 }
        };
    }

    public class ApplicationClient
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string AllowedOrigin { get; set; }

        public int RefreshTokenLifeTime { get; set; }
    }
}