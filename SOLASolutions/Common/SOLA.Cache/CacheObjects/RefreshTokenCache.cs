﻿using System.Collections.Generic;
using SOLA.Infrastructure.OAuth.Contracts;

namespace SOLA.Cache.CacheObjects
{
    public class RefreshTokenCache : Dictionary<string, RefreshToken>
    {
        public void Add(RefreshToken refreshToken)
        {
            Add(refreshToken.Token, refreshToken);
        }
    }
}