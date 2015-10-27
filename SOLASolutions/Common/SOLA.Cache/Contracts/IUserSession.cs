using System;

namespace SOLA.Cache.Contracts
{
    public interface IUserSession
    {
        Guid Id { get; set; }
        string UserAgent { get; set; }
        string CustomerSite { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        IRefreshToken RefreshToken { get;set; }
    }
}