using Application.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

[ExcludeFromCodeCoverage]
public class PersonEntity : BaseEntity
{
    public string Name { get; set; }

    public string Cpf { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string Address { get; set; }

    public int AddressNumber { get; set; }

    public string Complement { get; set; }

    public string Password { get; set; }

    public Collection<CreditCardEntity> CreditCards { get; set; } = new Collection<CreditCardEntity>();
}
