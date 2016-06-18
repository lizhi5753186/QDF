using System.Data.Entity;
using QDF.Entities;
using QDF.Repositories;

namespace QDF.EntityFramework.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepositoryWithTEntity<TEntity>
        where TEntity : class, IEntityWithPrimaryKey<int>
        where TDbContext : DbContext
    {
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}