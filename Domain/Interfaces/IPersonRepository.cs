using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersonRepository : IGenericRepository<PersonEntity>
{
    Task<bool> AuthenticatePerson(string cpf, string password);

    Task<PersonEntity> GetPersonByCpf(string cpf);
}
