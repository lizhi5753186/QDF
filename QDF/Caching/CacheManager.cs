using QDF.Caching.CacheProvider;
using QDF.Caching.Configuration;
using QDF.Dependency;

namespace QDF.Caching
{
    public class CacheManager
    {
        public static CacheManager Instance { get; private set; }

        static CacheManager()
        {
            Instance = new CacheManager();
        }

        public CacheManager()
        {
            Init();
        }

        public void Init()
        {
            CacheConfiguration.Init();
        }

        public ICacheProvider GetCacheProvider(string cacheType)
        {
            var cacheServerInfo = CacheConfiguration.CacheServerDictionary[cacheType];
            return CacheProviderFactory.GenerateCacheProvider(cacheServerInfo);
        }
    }
}