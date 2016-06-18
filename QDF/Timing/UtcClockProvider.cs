using System;

namespace QDF.Timing
{
    /// <summary>
    /// UTC 时间提供者
    /// </summary>
    public class UtcClockProvider
    {
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        /// <summary>
        /// 格式化为UTC时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime Normalize(DateTime dateTime)
        {
            switch (dateTime.Kind)
            {
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                case DateTimeKind.Local:
                    return dateTime.ToUniversalTime();
            }

            return dateTime;
        } 
    }
}