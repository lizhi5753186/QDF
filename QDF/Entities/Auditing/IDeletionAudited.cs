using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 删除审计
    /// </summary>
    public interface IDeletionAudited
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除的用户Id
        /// </summary>
        int? DeleteUserId { get; set; }
    }
}