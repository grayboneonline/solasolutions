using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLA.WebApi.TemporaryDatasource
{
    public class RefreshTokenDatasource
    {
        private static List<RefreshToken> data = new List<RefreshToken>();

        public static void Add(RefreshToken token)
        {
            data.Add(token);
        }

        public static int GetNewId()
        {
            return !data.Any() ? 1 : data.Max(x => x.Id) + 1;
        }

        public static RefreshToken GetByToken(string token)
        {
            return data.FirstOrDefault(x => x.Token == token);
        }

        public static void RemoveByToken(string token)
        {
            data.RemoveAll(x => x.Token == token);
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