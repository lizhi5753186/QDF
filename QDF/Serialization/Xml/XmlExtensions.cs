using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Xml.Serialization;
using Newtonsoft.Json.Schema;

namespace QDF.Serialization.Xml
{
    /// <summary>
    /// Xml 序列化和序列化扩展类
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// 从Xml 字符串转换为对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="xmlString">Xml 字符串</param>
        /// <returns>泛型对象</returns>
        public static T ToObject<T>(this string xmlString)
        {
            using (var stringReder = new StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                return (T) xmlSerializer.Deserialize(stringReder);
            }
        }

        /// <summary>
        /// 从字节流中得到对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="stream">字节流</param>
        /// <returns>泛型对象</returns>
        public static T ToObject<T>(this byte[] stream)
        {
            using (var ms = new MemoryStream(stream))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                return (T) xmlSerializer.Deserialize(ms);
            }
        }

        /// <summary>
        /// 从一个对象转换成Xml 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Xml 字符串</returns>
        public static string ToXml(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    var xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(stream, obj);
                    return sr.ReadToEnd();
                }
            }
        }
    }
}