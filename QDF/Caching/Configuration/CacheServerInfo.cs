using System;
using QDF.Caching.Enums;

namespace QDF.Caching.Configuration
{
    public class CacheServerInfo
    {
        public CacheType CacheType { get; set; }

        public string ServerHost { get; set; }

        /// <summary>
        /// 命名区域
        /// </summary>
        public string Region { get; set; }
        public TimeSpan DefaulExpirationTime { get; set; }

        public uint MaxPoolSize { get; set; }

        public int SendReceiveTimeout { get; set; }

        public int ConnectTimeout { get; set; }

        public uint MinPoolSize { get; set; }

        public bool IsDefault { get; set; }
    }
}