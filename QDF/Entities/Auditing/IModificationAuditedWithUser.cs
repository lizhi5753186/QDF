namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 更新跟踪接口
    /// </summary>
    /// <typeparam name="TUser">更新用户</typeparam>
    public interface IModificationAuditedWithUser<TUser> : IModificationAudited
        where TUser : IEntityWithPrimaryKey<int>
    {
        /// <summary>
        /// 更新的用户
        /// </summary>
        TUser UpdateUser { get; set; }
    }
}