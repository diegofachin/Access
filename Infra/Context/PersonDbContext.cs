using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
    {
        
    }

    public DbSet<PersonEntity> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersonDbContext).Assembly);
    }
}