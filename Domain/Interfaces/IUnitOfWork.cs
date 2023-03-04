namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository PersonRepository { get; }
    ICreditCardRepository CreditCardRepository { get; }

    int Commit();
}
