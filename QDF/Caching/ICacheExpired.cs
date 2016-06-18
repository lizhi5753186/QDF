using System;

namespace QDF.Caching
{
    public interface ICacheExpired
    {
        /// <summary>
        /// 默认为false
        /// </summary>
        bool IsExpired { get; set; }

        bool Sliding { get; set; }

        /// <summary>
        /// 绝对过期时间
        /// </summary>
        DateTime AbsoluteExpiration { get; }

        /// <summary>
        /// 滑动过期时间间隔
        /// </summary>
        TimeSpan SlidingExpiration { get; }
    }
}