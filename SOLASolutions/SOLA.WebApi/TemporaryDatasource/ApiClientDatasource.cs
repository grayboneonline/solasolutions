using System.Collections.Generic;
using System.Linq;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class ApiClientDatasource
    {
        private static List<ApiClient> data = new List<ApiClient>
        {
            new ApiClient{ Id = 1, ClientId = "webapp", Name = "AngularJs Single Page Web Application", IsActive = true, AllowedOrigin = "http://localhost", RefreshTokenLifeTime = 600 }
        };

        public static string[] GetAllClientId()
        {
            return data.Select(x => x.ClientId).ToArray();
        }

        public static ApiClient GetApiClientByClientId(string clientId)
        {
            return data.FirstOrDefault(x => x.ClientId == clientId);
        }
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