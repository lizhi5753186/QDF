using System;
using System.Runtime.Caching;
using QDF.Caching.Configuration;

namespace QDF.Caching.CacheProvider
{
    public class LocalCacheProvider : ICacheProvider
    {
        private readonly ObjectCache _localCache;

        private readonly string _region;
        private static TimeSpan DefaultExpirationTime { get; set; }

        public LocalCacheProvider()
        {
        }

        public LocalCacheProvider(CacheServerInfo serverInfo)
            : this(serverInfo.Region, serverInfo.DefaulExpirationTime)
        {

        }

        public LocalCacheProvider(string region, TimeSpan defaultExpirationTime)
        {
            DefaultExpirationTime = defaultExpirationTime;
            _region = region;
            _localCache = MemoryCache.Default;
        }

        public void Add(string key, object value, ICacheExpired cacheExpired)
        {
            var policy = new CacheItemPolicy();
            if (cacheExpired.IsExpired)
            {
                if (cacheExpired.Sliding)
                    policy.SlidingExpiration = cacheExpired.SlidingExpiration;
                else
                {
                    policy.AbsoluteExpiration = cacheExpired.AbsoluteExpiration;
                }
            }

            _localCache.Set(key, value, policy, _region);
        }

        public void Add(string key, object value, bool isExpired = false)
        {
            var policy = new CacheItemPolicy();
            if (isExpired)
                policy.SlidingExpiration = DefaultExpirationTime;

            _localCache.Set(key, value, policy, _region);
        }

        public void Get(string key)
        {
            _localCache.Get(key, _region);
        }

        public void Remove(string key)
        {
            _localCache.Remove(key, _region);
        }

        public void RemovePrefix(string prefixKey)
        {
             
        }
    }
}