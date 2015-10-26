namespace SOLA.Cache.Contracts
{
    public interface IUserSession
    {
        int UserId { get; set; }
        string UserName { get; set; }
        IRefreshToken RefreshToken { get;set; }
    }
}