using System;
using System.Threading.Tasks;

namespace QDF.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsTransactional { get; }

        bool Committed { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}