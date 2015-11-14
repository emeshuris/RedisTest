using System;
using System.Runtime.Caching;

namespace RedisConnection
{
    public class ApplicationCache
    {
        public readonly static string _cacheName = "Offers";

        #region Static Instance Members
        static readonly ICache _cache = new RedisCache();
        #endregion

        #region Enums
        /// <summary>
        /// Cache expiration type 
        /// </summary>
        public enum CacheExpirationType
        {
            Absolute,
            Sliding
        }
        #endregion

        #region Static Instance Methods
        /// <summary>
        /// Get cache item by key
        /// </summary>
        /// <typeparam name="T">cahe itme type</typeparam>
        /// <param name="key">cache key</param>
        /// <returns>cache item of type T</returns>
        public static T Get<T>(string key)
        {
            CacheItem item = null;
            try
            {
                item = _cache.GetCacheItem(key);
            }
            catch
            {
                // ignored
            }

            return item == null ? default(T) : (T)item.Value;
        }

        /// <summary>
        /// Set cache item
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="cacheItem">cache item</param>
        /// <param name="expirationType">expiration type</param>
        /// <param name="expirationTimeSpan">expiration time span</param>
        /// <param name="cachePriority">cache priority</param>
        /// <param name="changeMonitor">cache monitor call back</param>
        /// <param name="removeCallback">remove cache item call back</param>
        /// <param name="updateCallback">update cache item call back</param>
        public static void Set(
            string cacheKey,
            object cacheItem,
            CacheExpirationType expirationType,
            TimeSpan expirationTimeSpan,
            CacheItemPriority cachePriority = CacheItemPriority.Default,
            ChangeMonitor changeMonitor = null,
            CacheEntryRemovedCallback removeCallback = null,
            CacheEntryUpdateCallback updateCallback = null)
        {
            if (cacheItem == null)
            {
                return;
            }

            CacheItemPolicy policy = new CacheItemPolicy
            {
                Priority = cachePriority
            };

            if (expirationType == CacheExpirationType.Absolute)
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(expirationTimeSpan);
            }
            else
            {
                policy.SlidingExpiration = expirationTimeSpan;
            }

            if (changeMonitor != null)
            {
                policy.ChangeMonitors.Add(changeMonitor);
            }

            policy.RemovedCallback = removeCallback;
            policy.UpdateCallback = updateCallback;

            try
            {
                _cache.Set(cacheKey, cacheItem, policy);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Remove cache item by key
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        public static void Remove(string cacheKey)
        {
            try
            {
                if (_cache.Contains(cacheKey))
                {
                    _cache.Remove(cacheKey);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void Clear()
        {
            _cache.Dispose();
        }
        #endregion
    }
}
