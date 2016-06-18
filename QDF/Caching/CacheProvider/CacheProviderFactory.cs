using QDF.Caching.Configuration;
using QDF.Caching.Enums;
using QDF.Exceptions;

namespace QDF.Caching.CacheProvider
{
    public class CacheProviderFactory
    {
        public static ICacheProvider GenerateCacheProvider(CacheServerInfo configInfo)
        {
            switch (configInfo.CacheType)
            {
                case CacheType.Local:
                    return new LocalCacheProvider(configInfo);
                case CacheType.Memcache:
                    return new MemcachedProvider(configInfo);
                   
                case CacheType.Redis:
                    return new RedisCacheProvider(configInfo);
                default:
                    throw new QdfException("Error CacheType");
            }
        }
    }
}