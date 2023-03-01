using Application.Enums;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Infra.Repositories;

public class PersonRepository : GenericRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(PersonDbContext context) : base(context)
    {
    }

    public async Task<bool> AuthenticatePerson(string cpf, string password)
    {
        var authenticate = await _context.Persons.FirstOrDefaultAsync(x => x.Cpf == cpf && x.Password == password);

        if (authenticate == null)
            return false;

        return true;
    }
}
