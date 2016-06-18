using System.Threading.Tasks;
using System.Transactions;
using QDF.Dependency;

namespace QDF.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager, ITransientDependency
    {
        
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;

        public UnitOfWorkManager(IUnitOfWorkProvider unitOfWorkProvider)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        public IUnitOfWork Current 
        {
            get { return _unitOfWorkProvider.Current; }
        }

        public void Commit()
        {
            if (Current.IsTransactional)
            {
                using (var transactionscope =new TransactionScope())
                {
                    Current.SaveChanges();
                    transactionscope.Complete();
                }
            }
            else
            {
                Current.SaveChanges();
            }
        }

        public Task CompleteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}