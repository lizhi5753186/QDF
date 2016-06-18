using System.Data.Entity;
using QDF.EntityFramework.EntityFramework.Uow;
using QDF.Uow;

namespace QDF.EntityFramework.EntityFramework
{
    /// <summary>
    /// Default DbContext Provider.
    /// </summary>
    /// <typeparam name="TDbContext">Db Context</typeparam>
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext 
        {
            get { return _unitOfWorkProvider.Current.GetDbContext<TDbContext>(); }
        }

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        public DefaultDbContextProvider(IUnitOfWorkProvider unitOfWorkProvider)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }
    }
}