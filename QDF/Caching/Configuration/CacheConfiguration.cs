using System;
using System.Collections.Generic;
using QDF.Caching.Enums;
using QDF.Configuration;
using QDF.Exceptions;

namespace QDF.Caching.Configuration
{
    public class CacheConfiguration
    {
        public static Dictionary<string, CacheServerInfo> CacheServerDictionary { get; set; }

        public CacheConfiguration()
        {
            CacheServerDictionary = new Dictionary<string, CacheServerInfo>();
        }

        public static void Init()
        {
            var configElement = ConfigManager.LoadConfig();
            var root = configElement.XElement.Element("Caches");
            if(root == null)
                throw new QdfException("CacheStorages node not exists");
            foreach (var ele in root.Elements("Cache"))
            {
                var type = ele.Attribute("type").Value;
                var isEnable = Convert.ToBoolean(ele.Attribute("isEnable").Value);
                var xServerHostEle = ele.Element("ServerHost");
                var xRegionEle = ele.Element("Region");
                var xExpireTime = ele.Element("ExpirationTime");
                var xMaxPoolSize = ele.Element("MaxPoolSize");
                var xMinPoolSize = ele.Element("MinPoolSize");
                var xSendReceiveTimeout = ele.Element("SendReveiveTimeout");
                var xConnectTimeout = ele.Element("ConnectTimeout");

                if (xServerHostEle == null)
                    throw new QdfException("ServerHost node not exists");
                
                var cacheServerInfo = new CacheServerInfo
                {
                    CacheType = (CacheType) Enum.Parse(typeof (CacheType), type),
                    ServerHost = xServerHostEle.Attribute("value").Value,
                    Region = xRegionEle == null ? null: xRegionEle.Attribute("value").Value,
                    DefaulExpirationTime = xExpireTime == null ? new TimeSpan(0, 30, 0) : new TimeSpan(0, int.Parse(xExpireTime.Attribute("value").Value), 0),
                    MaxPoolSize = xMaxPoolSize == null ? 1000 :  uint.Parse(xMaxPoolSize.Attribute("value").Value),
                    MinPoolSize = xMinPoolSize == null ? 10 : uint.Parse(xMinPoolSize.Attribute("value").Value),
                    SendReceiveTimeout = xSendReceiveTimeout == null ? 500 : int.Parse(xSendReceiveTimeout.Attribute("value").Value),
                    ConnectTimeout = xConnectTimeout == null ? 500 : int.Parse(xConnectTimeout.Attribute("value").Value),
                    IsDefault = isEnable,
                };

                if (!CacheServerDictionary.ContainsKey(type))
                    CacheServerDictionary.Add(type, cacheServerInfo);
            }
        }
    }
}