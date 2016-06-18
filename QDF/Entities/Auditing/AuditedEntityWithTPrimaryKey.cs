using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// IAudited ʵ����
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Serializable]
    public abstract class AuditedEntityWithTPrimaryKey<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        public virtual DateTime UpdateTime { get; set; }

        public virtual int? UpdateUserId { get; set; }

    }
}