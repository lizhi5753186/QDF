namespace QDF.Caching
{
    public interface ICacheProvider
    {
        void Add(string key, object value, ICacheExpired cacheExpired);

        void Add(string key, object value, bool isExpired = false);

        void Get(string key);

        void Remove(string key);

        void RemovePrefix(string prefixKey);
    }
}