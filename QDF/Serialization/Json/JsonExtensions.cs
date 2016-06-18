using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace QDF.Serialization.Json
{
    /// <summary>
    /// JSON 序列化与反序列化扩展类
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 从一个对象信息生成Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="camelCase">是否支持驼峰式大小写转换</param>
        /// <param name="indented">是否缩进，使用缩进可以有更好的可读性，不使用可以减少JSON的大小，传输过程中减少带宽</param>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }

        /// <summary>
        /// 从一个对象生成Json字符串，其日期类型转换成"yyyy-MM-dd HH:mm:ss"这样的形式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="camelCase">是否支持驼峰式大小写转换</param>
        /// <param name="indented">是否缩进，使用缩进可以有更好的可读性，不使用可以减少JSON的大小，传输过程中减少带宽</param>
        /// <returns></returns>
        public static string ToJsonStringForTimeFormat(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            options.Converters = new[] {timeFormat};
            return JsonConvert.SerializeObject(obj, options);
        }

        /// <summary>
        /// 从Json字符串生成对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="jsonString">JSON 字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 从Json字符串生成对象，日期对象的格式为"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="jsonString">Json 字符串</param>
        /// <returns></returns>
        public static T ToObjectForTimeFormat<T>(this string jsonString)
        {
            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.DeserializeObject<T>(jsonString, timeFormat);
        }
    }
}