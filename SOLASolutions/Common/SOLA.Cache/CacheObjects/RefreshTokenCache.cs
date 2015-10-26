using System.Collections.Generic;
using SOLA.Cache.Contracts;

namespace SOLA.Cache.CacheObjects
{
    public class RefreshTokenCache : Dictionary<string, IRefreshToken>
    {
        public void Add(IRefreshToken refreshToken)
        {
            Add(refreshToken.Token, refreshToken);
        }
    }
}