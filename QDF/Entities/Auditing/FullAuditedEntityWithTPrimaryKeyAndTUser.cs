using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// IFullAuditedWithTUser й╣ож
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    [Serializable]
    public abstract class FullAuditedEntityWithTPrimaryKeyAndTUser<TPrimaryKey, TUser> : AuditedEntityWithTPrimaryKeyAndTUser<TPrimaryKey, TUser>, IFullAuditedWithTUser<TUser>
        where TUser : IEntityWithPrimaryKey<int>
    {
        public virtual bool IsDeleted { get; set; }

        [ForeignKey("DeleteUserId")]
        public virtual TUser DeleteUser { get; set; }

        public virtual int? DeleteUserId { get; set; }

        public virtual DateTime? DeleteTime { get; set; }
    }
}
