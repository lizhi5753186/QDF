namespace QDF.Entities.Auditing
{
    public interface IAuditedWithTUser<TUser> : IAudited, ICreateAuditedWithUser<TUser>,
        IModificationAuditedWithUser<TUser>
        where TUser : IEntityWithPrimaryKey<int>
    {

    }
}