using System;

namespace QDF.Caching
{
    public class CacheExpired : ICacheExpired
    {
        public bool IsExpired { get; set; }

        public bool Sliding { get; set; }

        public DateTime AbsoluteExpiration { get; private set; }

        public TimeSpan SlidingExpiration { get; private set; }

        public CacheExpired()
        {
            IsExpired = false;
        }

        public static ICacheExpired Create(DateTime absoluteExpiration)
        {
            return new CacheExpired
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = TimeSpan.MaxValue,
                Sliding = false,
                IsExpired = true
            };
        }

        public static ICacheExpired Create(TimeSpan slidingExpiration)
        {
            return new CacheExpired
            {
                SlidingExpiration = slidingExpiration,
                AbsoluteExpiration = DateTime.Now.Add(slidingExpiration),
                Sliding = true,
                IsExpired = true
            };
        }
    }
}