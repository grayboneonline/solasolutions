using System;
using System.Collections.Generic;

namespace SOLA.MemoryCache
{
    public interface ICacheManager
    {
        void Initialize();
        void Set<T>(CacheKey key, T data, bool overwrite = false);
        T Get<T>(CacheKey key);
    }

    public class CacheManager : ICacheManager
    {
        private Dictionary<CacheKey, CacheObject> memoryCache;

        public void Initialize()
        {
            memoryCache = new Dictionary<CacheKey, CacheObject>();
        }

        public void Set<T>(CacheKey key, T data, bool overwrite = false)
        {
            var cacheObj = CacheObject<T>.Create(data);
            if (memoryCache.ContainsKey(key))
            {
                if (overwrite) memoryCache[key] = cacheObj;
                else throw new ArgumentException(key + " already exists.");
            }
            else
                memoryCache.Add(key, cacheObj);
        }

        public T Get<T>(CacheKey key)
        {
            if (!memoryCache.ContainsKey(key))
                throw new ArgumentException(key + " is not existed.");
            var cacheObj = memoryCache[key] as CacheObject<T>;
            if (cacheObj == null)
                throw new InvalidCastException("Invalid type for " + key);

            return cacheObj.Data;
        }
    }

    public enum CacheKey
    {
        ApiClients = 1,
        RefreshTokens = 2,
        UserSessions = 3,
    }
}
