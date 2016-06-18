using System.Collections.Generic;
using QDF.Caching.Configuration;
using ServiceStack.Redis;

namespace QDF.Caching.CacheProvider
{
    public class RedisCacheProvider : ICacheProvider
    {
        public static Dictionary<string, Dictionary<long, PooledRedisClientManager>> PooledRedisClientManager =
            new Dictionary<string, Dictionary<long, PooledRedisClientManager>>();

        public static Dictionary<string, Dictionary<long, RedisClient>> PersistentClient =
            new Dictionary<string, Dictionary<long, RedisClient>>();

        private readonly CacheServerInfo _cacheServerInfo;

        public RedisCacheProvider(CacheServerInfo cacheServerInfo)
        {
            _cacheServerInfo = cacheServerInfo;
            var clientManagerConfig = new RedisClientManagerConfig();
            clientManagerConfig.MaxWritePoolSize = (int) cacheServerInfo.MaxPoolSize;
            clientManagerConfig.MaxReadPoolSize = (int) cacheServerInfo.MaxPoolSize;
            clientManagerConfig.AutoStart = true;
            var dictionary = new Dictionary<long, PooledRedisClientManager>
            {
                {0, new PooledRedisClientManager(cacheServerInfo.ServerHost.Split(','),cacheServerInfo.ServerHost.Split(','), clientManagerConfig)}
            };
            PooledRedisClientManager.Add(cacheServerInfo.CacheType.ToString(), dictionary);
        }

        public void Add(string key, object value, ICacheExpired cacheExpired)
        {
            throw new System.NotImplementedException();
        }

        public void Add(string key, object value, bool isExpired = false)
        {
            throw new System.NotImplementedException();
        }

        public void Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public void RemovePrefix(string prefixKey)
        {
            throw new System.NotImplementedException();
        }
    }
}