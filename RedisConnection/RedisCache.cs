using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Runtime.Caching;

namespace RedisConnection
{
    public class RedisCache : ICache
    {
        private const string REDIS_CONNECTION_STRING = "192.168.9.13:6379";

        private ConnectionMultiplexer _connectionMultiplexer;

        private ConnectionMultiplexer ConnectionMultiplexer
        {
            get
            {
                return _connectionMultiplexer ?? (_connectionMultiplexer = ConnectionMultiplexer.Connect(REDIS_CONNECTION_STRING));
            }
        }

        private IDatabase RedisDatabase
        {
            get
            {
                return ConnectionMultiplexer.GetDatabase();
            }
        }

        public bool Contains(string key, string regionName = null)
        {
            return RedisDatabase.KeyExists(key);
        }

        public CacheItem GetCacheItem(string key, string regionName = null)
        {
            CacheItem cacheItem = new CacheItem(key);
            RedisValue redisValue = RedisDatabase.StringGet(key);

            if (redisValue.IsNullOrEmpty || !redisValue.HasValue)
            {
                return null;
            }

            cacheItem.Value = redisValue;
            return cacheItem;
        }

        public object Remove(string key, string regionName = null)
        {
            return RedisDatabase.KeyDelete(key);
        }

        public void Set(string key, object value, CacheItemPolicy cacheItemPolicy, string regionName = null)
        {
            Set(new CacheItem(key, value, regionName), cacheItemPolicy);
        }

        public void Set(CacheItem cacheItem, CacheItemPolicy cacheItemPolicy)
        {
            string serializedValue = JsonConvert.SerializeObject(cacheItem.Value);
            RedisDatabase.StringSet(cacheItem.Key, serializedValue);
        }

        public void Dispose()
        {
            _connectionMultiplexer.Dispose();
            _connectionMultiplexer = null;
        }
    }
}
