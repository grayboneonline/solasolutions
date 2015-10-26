using System;
using SOLA.Cache.Contracts;

namespace SOLA.WebApi.Models.OAuth
{
    public class UserSession : IUserSession
    {
        public Guid Id { get; set; }
        public string UserAgent { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IRefreshToken RefreshToken { get; set; }
    }
}