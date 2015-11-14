using System;
using System.Runtime.Caching;

namespace RedisConnection
{
    public interface ICache : IDisposable
    {
        CacheItem GetCacheItem(string key, string regionName = null);

        void Set(CacheItem item, CacheItemPolicy policy);

        void Set(string key, object value, CacheItemPolicy policy, string regionName = null);

        bool Contains(string key, string regionName = null);

        object Remove(string key, string regionName = null);
    }

}
