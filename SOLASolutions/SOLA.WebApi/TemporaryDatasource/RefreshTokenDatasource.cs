using System;
using System.Collections.Generic;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class RefreshTokenCache : Dictionary<string, RefreshToken>
    {
        public void Add(RefreshToken refreshToken)
        {
            Add(refreshToken.Token, refreshToken);
        }
    }

    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string Subject { get; set; }

        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }
    }
}