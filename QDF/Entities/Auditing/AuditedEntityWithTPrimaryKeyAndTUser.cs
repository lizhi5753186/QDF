using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QDF.Entities.Auditing
{
    /// <summary>
    /// IAuditedWithTUser й╣ож
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    [Serializable]
    public abstract class AuditedEntityWithTPrimaryKeyAndTUser<TPrimaryKey, TUser> : AuditedEntityWithTPrimaryKey<TPrimaryKey>, IAuditedWithTUser<TUser>
        where TUser : IEntityWithPrimaryKey<int>
    {
        [ForeignKey("CreateUserId")]
        public virtual TUser CreateUser { get; set; }

        [ForeignKey("UpdateUserId")]
        public virtual TUser UpdateUser { get; set; }
    }
}