namespace SOLA.Infrastructure.OAuth.Contracts
{
    public interface IClientInfo
    {
        bool IsActive { get; set; }
        string AllowedOrigin { get; set; }
        int RefreshTokenLifeTime { get; set; }
    }
}
