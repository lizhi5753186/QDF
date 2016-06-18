using System.Threading.Tasks;

namespace QDF.Uow
{
    public class NullUnitOfWork : IUnitOfWork
    {
        public bool IsTransactional
        {
            get { return false; }
        }

        public bool Committed
        {
            get { return false; }
        }

        public void SaveChanges()
        {
        }

        public async Task SaveChangesAsync()
        {
        }

        public void Dispose()
        {
            
        }
    }
}