using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// ICreateAuditedWithUser 实现类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    /// <typeparam name="TUser">实体类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityWithTPrimaryKeyAndTUser<TPrimaryKey, TUser> : CreationAuditedEntity<TPrimaryKey>, ICreateAuditedWithUser<TUser>
        where TUser : IEntityWithPrimaryKey<int>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreateUser { get; set; }
    }
}