using QDF.Dependency;

namespace QDF.Uow
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider, ITransientDependency
    {
        public IUnitOfWork Current { get; set; }
    }
}