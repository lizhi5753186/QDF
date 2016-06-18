using QDF.Entities;

namespace QDF.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    public interface IRepositoryWithTEntity<TEntity> : IRepositoryWithTEntityAndTPrimaryKey<TEntity, int> 
        where TEntity : class,  IEntityWithPrimaryKey<int>
    {
    }
}
