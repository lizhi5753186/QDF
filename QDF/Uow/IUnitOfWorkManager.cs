using System.Threading.Tasks;

namespace QDF.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get;  }

        void Commit();

        Task CompleteAsync();
    }
}