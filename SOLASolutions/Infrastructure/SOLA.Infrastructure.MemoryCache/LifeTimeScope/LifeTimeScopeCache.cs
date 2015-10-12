using System;
using System.Collections.Generic;

namespace SOLA.Infrastructure.MemoryCache.LifeTimeScope
{
    public interface ILifeTimeScopeCache
    {
        void Initialize();
        void Set<T>(LifeTimeScopeCacheKey key, T data, bool overwrite = false);
        T Get<T>(LifeTimeScopeCacheKey key);
    }

    public class LifeTimeScopeCache : ILifeTimeScopeCache
    {
        private Dictionary<LifeTimeScopeCacheKey, CacheObject> memoryCache;

        public void Initialize()
        {
            memoryCache = new Dictionary<LifeTimeScopeCacheKey, CacheObject>();
        }

        public void Set<T>(LifeTimeScopeCacheKey key, T data, bool overwrite = false)
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

        public T Get<T>(LifeTimeScopeCacheKey key)
        {
            if (!memoryCache.ContainsKey(key))
                throw new ArgumentException(key + " is not existed.");
            var cacheObj = memoryCache[key] as CacheObject<T>;
            if (cacheObj == null)
                throw new InvalidCastException("Invalid type for " + key);

            return cacheObj.Data;
        }
    }

    public enum LifeTimeScopeCacheKey
    {
        ApplicationClients = 1,
        RefreshTokens = 2,
    }
}
