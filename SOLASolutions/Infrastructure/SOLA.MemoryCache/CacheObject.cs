namespace SOLA.MemoryCache
{
    public class CacheObject
    {
        
    }

    public class CacheObject<T> : CacheObject
    {
        public T Data { get; set; }

        public static CacheObject<T> Create(T data)
        {
            return new CacheObject<T> {Data = data};
        } 
    }
}
