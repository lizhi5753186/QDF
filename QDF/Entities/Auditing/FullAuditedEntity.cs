using System;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntity : FullAuditedEntityWithTPrimaryKey<int>
    {

    }
}