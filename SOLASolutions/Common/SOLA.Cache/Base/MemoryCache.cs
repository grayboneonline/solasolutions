using System;
using System.Collections.Generic;

namespace SOLA.Cache.Base
{
    public interface IMemoryCache
    {
    }

    public class MemoryCache : IMemoryCache
    {
        protected Dictionary<Type, object> Cache = new Dictionary<Type, object>();

        protected void Set<T>(object data)
        {
            if (Cache.ContainsKey(typeof(T)))
                Cache[typeof(T)] = data;
            else
                Cache.Add(typeof(T), data);
        }

        protected T Get<T>() where T : class
        {
            return Get<T, T>();
        }

        protected TAs Get<T, TAs>() where TAs : class
        {
            if (!Cache.ContainsKey(typeof(T)))
                throw new ArgumentException(typeof(T) + " is not existed.");
            var cacheObj = Cache[typeof(T)] as TAs;
            if (cacheObj == null)
                throw new InvalidCastException("Invalid type for " + typeof(T));

            return cacheObj;
        }
    }
}
