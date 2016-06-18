using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// ICreateAuditedWithUser ʵ����
    /// </summary>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TUser">ʵ������</typeparam>
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