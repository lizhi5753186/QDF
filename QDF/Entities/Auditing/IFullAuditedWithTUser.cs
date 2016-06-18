namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 所有跟踪接口
    /// </summary>
    /// <typeparam name="TUser">实体对象</typeparam>
    public interface IFullAuditedWithTUser<TUser> : IAuditedWithTUser<TUser>, IFullAudited,
        IDeletionAuditedWithUser<TUser>
        where TUser : IEntityWithPrimaryKey<int>
    {

    }
}