using System;
using QDF.Timing;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// ICreationAudited �ӿ�ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : EntityWithPrimaryKey<TPrimaryKey>, ICreationAudited
    {
        public virtual DateTime CreateTime { get; set; }
        public virtual int? CreateUserId { get; set; }

        protected CreationAuditedEntity()
        {
            CreateTime = ClockManager.Now;
        }
    }
}