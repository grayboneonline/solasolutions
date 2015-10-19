namespace SOLA.Cache.Contracts
{
    public interface IApplicationClient
    {
        int Id { get; set; }
        string ClientId { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }
        string AllowedOrigin { get; set; }
        int RefreshTokenLifeTime { get; set; } 
    }
}