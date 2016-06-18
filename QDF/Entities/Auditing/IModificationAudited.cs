using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 更新跟踪接口
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新的用户Id
        /// </summary>
        int? UpdateUserId { get; set; }
    }
}