using System;
using System.Collections.Generic;

namespace SOLA.MemoryCache
{
    public interface ICacheManager
    {
        void Set<T>(CacheKey key, T data, bool overwrite = false);
        T Get<T>(CacheKey key);
    }

    public class CacheManager : ICacheManager
    {
        private Dictionary<CacheKey, CacheObject> memoryCache;

        public Dictionary<CacheKey, CacheObject> MemoryCache
        {
            get { return memoryCache ?? (memoryCache = new Dictionary<CacheKey, CacheObject>()); }
        }

        public void Set<T>(CacheKey key, T data, bool overwrite = false)
        {
            var cacheObj = CacheObject<T>.Create(data);
            if (MemoryCache.ContainsKey(key))
            {
                if (overwrite) MemoryCache[key] = cacheObj;
                else throw new ArgumentException(key + " already exists.");
            }
            else
                MemoryCache.Add(key, cacheObj);
        }

        public T Get<T>(CacheKey key)
        {
            if (!MemoryCache.ContainsKey(key))
                throw new ArgumentException(key + " is not existed.");
            var cacheObj = MemoryCache[key] as CacheObject<T>;
            if (cacheObj == null)
                throw new InvalidCastException("Invalid type for " + key);

            return cacheObj.Data;
        }
    }

    public enum CacheKey
    {
        ApiClients = 1,
        UserSessions = 2,
    }
}
