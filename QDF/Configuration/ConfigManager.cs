using System;
using System.IO;
using QDF.Utils;

namespace QDF.Configuration
{
    public class ConfigManager
    {
        public static IConfigElement LoadConfig()
        {
            var configElement = new ConfigElement
            {
                XElement =
                    XmlHelper.ToXElement(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QDF.config"))
            };

            return configElement;
        }
    }
}