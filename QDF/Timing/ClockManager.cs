using System;
using QDF.Exceptions;

namespace QDF.Timing
{
    /// <summary>
    /// 时钟管理类
    /// </summary>
    public class ClockManager
    {
        private static IClockProvider _provider; 
        
        /// <summary>
        /// 时钟提供者
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                {
                    throw new QdfException("Can not set Clock to null!");
                }

                _provider = value;
            }
        }

        static ClockManager()
        {
            Provider = new LocalClockProvider();
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime Now
        {
            get { return Provider.Now; }
        }

        /// <summary>
        /// 格式化时间为指定时钟时间
        /// </summary>
        /// <param name="dateTime">需要格式化的时间</param>
        /// <returns>当前时钟的时间</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}