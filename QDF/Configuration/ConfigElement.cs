using System.Xml;
using System.Xml.Linq;

namespace QDF.Configuration
{
    public class ConfigElement : IConfigElement
    {
        public bool IsXElementConfig 
        {
            get { return XElement != null; }
        }

        public string ConfigFile { get; set; }

        public XElement XElement { get; set; }

    }
}