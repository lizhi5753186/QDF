using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Castle.Core.Internal;
using QDF.Dependency;
using QDF.Reflection;
using QDF.Uow;

namespace QDF.EntityFramework.EntityFramework.Uow
{
    public class EfUnitOfWork : IUnitOfWork
    {
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        protected IIocResolver IocResolver { get; private set; }

        public EfUnitOfWork(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        public bool IsTransactional
        {
            get { return true; }
        }

        public bool Committed
        {
            get { return false; }
        }

        public void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach((dbContext) => dbContext.SaveChanges());
        }

        public Task SaveChangesAsync()
        {
            SaveChanges();
            return Task.FromResult(0);
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
           where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = IocResolver.Resolve<TDbContext>();

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        public void Dispose()
        {
            ActiveDbContexts.Values.ForEach(Release);
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }
    }
}