using System;
using System.Collections.Generic;

namespace QDF.Collections.Extensions
{
    /// <summary>
    /// 集合对象的扩展类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 检查集合是否为null 或 空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 如果不存在则添加进集合
        /// </summary>
        /// <param name="source">集合对象</param>
        /// <param name="item">添加的对象</param>
        /// <typeparam name="T">集合内的泛型对象</typeparam>
        /// <returns>添加成功返回True，已存在则返回false.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}