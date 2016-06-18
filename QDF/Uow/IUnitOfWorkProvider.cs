namespace QDF.Uow
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Current { get; set; }
    }
}