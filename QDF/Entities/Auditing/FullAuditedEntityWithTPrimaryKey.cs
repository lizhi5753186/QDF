using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// IFullAudited µÄÊµÏÖ
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Serializable]
    public abstract class FullAuditedEntityWithTPrimaryKey<TPrimaryKey> : AuditedEntityWithTPrimaryKey<TPrimaryKey>, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }
        
        public virtual int? DeleteUserId { get; set; }
        
        public virtual DateTime? DeleteTime { get; set; }
    }
}
