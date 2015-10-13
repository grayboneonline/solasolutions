using System;
using System.Collections.Generic;

namespace SOLA.Infrastructure.MemoryCache.RequestScope
{
    public interface IRequestScopeCache
    {
        void Set<T>(RequestScopeCacheKey key, T data, bool overwrite = false);
        T Get<T>(RequestScopeCacheKey key);
    }

    public class RequestScopeCache : IRequestScopeCache
    {
        private Dictionary<RequestScopeCacheKey, CacheObject> memoryCache;

        public void Set<T>(RequestScopeCacheKey key, T data, bool overwrite = false)
        {
            if (memoryCache == null)
                memoryCache = new Dictionary<RequestScopeCacheKey, CacheObject>();

            var cacheObj = CacheObject<T>.Create(data);
            if (memoryCache.ContainsKey(key))
            {
                if (overwrite) memoryCache[key] = cacheObj;
                else throw new ArgumentException(key + " already exists.");
            }
            else
                memoryCache.Add(key, cacheObj);
        }

        public T Get<T>(RequestScopeCacheKey key)
        {
            if (memoryCache == null)
                return default(T);

            if (!memoryCache.ContainsKey(key))
                throw new ArgumentException(key + " is not existed.");
            var cacheObj = memoryCache[key] as CacheObject<T>;
            if (cacheObj == null)
                throw new InvalidCastException("Invalid type for " + key);

            return cacheObj.Data;
        }
    }

    public enum RequestScopeCacheKey
    {
        CustomerDataSource = 1,
    }
}
