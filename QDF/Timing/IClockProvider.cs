using System;

namespace QDF.Timing
{
    /// <summary>
    /// 时钟提供者接口
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        DateTime Now { get;}

        /// <summary>
        /// 格式化给定的时间
        /// </summary>
        /// <param name="dateTime">需要格式化的DateTime对象</param>
        /// <returns>格式化后的DateTime对象</returns>
        DateTime Normalize(DateTime dateTime);
    }
}