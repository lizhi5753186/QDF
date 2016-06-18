using System;

namespace QDF.Timing
{
    /// <summary>
    /// 本地时间提供者
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        public DateTime Now { get { return DateTime.Now; } }

        /// <summary>
        /// 格式化为本地时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime Normalize(DateTime dateTime)
        {
            switch (dateTime.Kind)
            {
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
                case DateTimeKind.Utc:
                    return dateTime.ToLocalTime();
            }

            return dateTime;
        }
    }
}