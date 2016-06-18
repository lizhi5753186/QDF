using System;
using QDF.Caching.Enums;

namespace QDF.Caching.Attributes
{
    /// <summary>
    /// 缓存特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CacheAttribute : Attribute
    {
        #region Properties
        public CachingMethod Method { get; set; }

        public string CacheName { get; set; }

        // 缓存相关的方法名称，该参数仅在Remove的方式用到
        public string[] CorrespondingMethodNames { get; set; }

        #endregion

        #region Constructor
        public CacheAttribute(CachingMethod method)
        {
            CacheName = "redis";
            Method = method;
        }

        public CacheAttribute(CachingMethod method, string cacheName)
        {
            CacheName = cacheName;
            Method = method;
        }

        public CacheAttribute(CachingMethod method, params string[] correspondingMethodNames)
            : this(method)
        {
            CorrespondingMethodNames = correspondingMethodNames;
        }
        #endregion
    }
}