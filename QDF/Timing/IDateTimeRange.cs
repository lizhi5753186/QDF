using System;

namespace QDF.Timing
{
    /// <summary>
    /// 时间范围接口
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; set; } 
    }
}