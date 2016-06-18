using System.Xml;
using System.Xml.Linq;

namespace QDF.Utils
{
    public class XmlHelper
    {
        public static XmlElement ToXmlElement(string xmlString, bool isFilePath)
        {
            var xmlDocument = new XmlDocument();
            if (!isFilePath)
                xmlDocument.LoadXml(xmlString);
            else
                xmlDocument.Load(xmlString);
            return xmlDocument.DocumentElement;
        }

        public static XmlElement ToXmlElement(string xmlString)
        {
            return ToXmlElement(xmlString, false);
        }

        public static string GetNodeValue(XmlNode config)
        {
            if (config == null)
                return string.Empty;
            if (config is XmlAttribute || config is XmlText)
                return config.Value;
            return config.InnerXml;
        }

        public static XmlElement ToXmlElement(XElement xElement)
        {
            if (xElement == null)
                return null;
            var xmlElement = (XmlElement)null;
            var xmlReader = (XmlReader)null;
            try
            {
                xmlReader = xElement.CreateReader();
                xmlElement = new XmlDocument().ReadNode(xElement.CreateReader()) as XmlElement;
            }
            catch
            {
            }
            finally
            {
                if (xmlReader != null)
                    xmlReader.Close();
            }
            return xmlElement;
        }

        public static XElement ToXElement(XmlElement xmlElement)
        {
            if (xmlElement == null)
                return null;
            var xelement = (XElement)null;
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.AppendChild(xmlDocument.ImportNode(xmlElement, true));
                xelement = XElement.Parse(xmlDocument.InnerXml);
            }
            catch
            {
                // ignored
            }
            return xelement;
        }

        public static XElement ToXElement(string xmlPath)
        {
            return XElement.Load(xmlPath);
        }
    }
}