using System;

namespace SOLA.Cache.Contracts
{
    public interface IRefreshToken
    {
        string Token { get; set; }
        string Subject { get; set; }
        string ClientId { get; set; }
        DateTime IssuedUtc { get; set; }
        DateTime ExpiresUtc { get; set; }
        string ProtectedTicket { get; set; }
    }
}