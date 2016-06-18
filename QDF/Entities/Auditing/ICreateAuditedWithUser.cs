namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 创建实体跟踪
    /// </summary>
    /// <typeparam name="TUser">创建实体的用户</typeparam>
    public interface ICreateAuditedWithUser<TUser> : ICreationAudited
        where TUser : IEntityWithPrimaryKey<int>
    {
        /// <summary>
        /// 创建实体的用户
        /// </summary>
        TUser CreateUser { get; set; }
    }
}