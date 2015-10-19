using System;
using SOLA.Cache.Contracts;

namespace SOLA.WebApi.Models.OAuth
{
    public class RefreshToken : IRefreshToken
    {
        public string Token { get; set; }
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
