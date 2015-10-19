namespace SOLA.DataAccess.Models.Admin
{
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
