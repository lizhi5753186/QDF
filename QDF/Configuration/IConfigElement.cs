using System.Xml.Linq;

namespace QDF.Configuration
{
    public interface IConfigElement
    {
        bool IsXElementConfig { get; }

        string ConfigFile { get; set; }

        XElement XElement { get; set; } 
    }
}