namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository PersonRepository { get; }

    int Commit();
}
