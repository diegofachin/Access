using Application.Enums;
using BCrypt.Net;
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
        var person = await _context.Persons.FirstOrDefaultAsync(x => x.Cpf == cpf);
        if (person == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, person.Password);
    }

    public async Task<PersonEntity> GetPersonByCpf(string cpf)
    {
        return await _context.Persons.FirstOrDefaultAsync(x => x.Cpf == cpf);
    }

}
