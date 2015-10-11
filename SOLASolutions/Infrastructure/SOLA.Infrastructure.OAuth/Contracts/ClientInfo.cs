namespace SOLA.Infrastructure.OAuth.Contracts
{
    public class ClientInfo
    {
        public bool IsActive { get; set; }
        public string AllowedOrigin { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
