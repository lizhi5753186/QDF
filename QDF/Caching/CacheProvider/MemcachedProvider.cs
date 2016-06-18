using System;
using BeIT.MemCached;
using QDF.Caching.Configuration;

namespace QDF.Caching.CacheProvider
{
    public class MemcachedProvider : ICacheProvider
    {
        private readonly CacheServerInfo _cacheServerInfo;
        private MemcachedClient memcachedClient;


        public MemcachedProvider(CacheServerInfo cacheServerInfo)
        {
            _cacheServerInfo = cacheServerInfo;
            MemcachedClient.Setup(cacheServerInfo.CacheType.ToString(), cacheServerInfo.ServerHost.Split(','));
            memcachedClient = MemcachedClient.GetInstance(cacheServerInfo.CacheType.ToString());
            memcachedClient.SendReceiveTimeout = _cacheServerInfo.SendReceiveTimeout;
            memcachedClient.ConnectTimeout = _cacheServerInfo.ConnectTimeout;
            memcachedClient.MinPoolSize = _cacheServerInfo.MinPoolSize;
            memcachedClient.MaxPoolSize = _cacheServerInfo.MaxPoolSize;
            memcachedClient.KeyPrefix = _cacheServerInfo.Region;
        }

        public void Add(string key, object value, ICacheExpired cacheExpired)
        {
            if (cacheExpired.IsExpired)
            {
                if (cacheExpired.Sliding)
                    memcachedClient.Set(key, value, cacheExpired.SlidingExpiration);
                else
                {
                    memcachedClient.Set(key, value, cacheExpired.AbsoluteExpiration);
                }
            }
            else
            {
                memcachedClient.Set(key, value);
            }
        }

        public void Add(string key, object value, bool isExpired = false)
        {
            if (isExpired)
                memcachedClient.Set(key, value, _cacheServerInfo.DefaulExpirationTime);
            else
                memcachedClient.Set(key, value);
        }

        public void Get(string key)
        {
            memcachedClient.Get(key);
        }

        public void Remove(string key)
        {
            memcachedClient.Delete(key);
        }

        public void RemovePrefix(string prefixKey)
        {
            
        }
    }
}