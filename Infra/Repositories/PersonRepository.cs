using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;

namespace Infra.Repositories;

public class PersonRepository : GenericRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(PersonDbContext context) : base(context)
    {
    }
}
