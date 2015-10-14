using System.Collections.Generic;
using SOLA.Models.Admin;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class ApplicationClientDatasource
    {
        public static List<ApplicationClient> Data = new List<ApplicationClient>
        {
            new ApplicationClient{ Id = 1, ClientId = "webapp", Name = "AngularJs Single Page Web Application", IsActive = true, AllowedOrigin = "http://localhost", RefreshTokenLifeTime = 600 }
        };
    }
}