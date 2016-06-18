namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 删除跟踪的接口
    /// </summary>
    /// <typeparam name="TUser">实体类型</typeparam>
    public interface IDeletionAuditedWithUser<TUser> : IDeletionAudited
        where TUser : IEntityWithPrimaryKey<int>
    {
        /// <summary>
        /// 删除实体的用户
        /// </summary>
        TUser DeleteUser { get; set; }
    }
}