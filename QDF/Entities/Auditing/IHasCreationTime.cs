using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 创建时间接口
    /// 如果实体需要跟踪其创建时间的话，就必须实现该接口
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 实体的创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}