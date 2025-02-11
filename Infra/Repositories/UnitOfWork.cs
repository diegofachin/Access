﻿using Domain.Interfaces;
using Infra.Context;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Repositories;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly PersonDbContext _context;
    public IPersonRepository PersonRepository { get; }

    public ICreditCardRepository CreditCardRepository { get; }

    public UnitOfWork(PersonDbContext context, IPersonRepository personRepository, ICreditCardRepository creditCardRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        PersonRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        CreditCardRepository = creditCardRepository;
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
