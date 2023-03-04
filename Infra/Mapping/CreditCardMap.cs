using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Mapping;

[ExcludeFromCodeCoverage]
public class CreditCardMap : IEntityTypeConfiguration<CreditCardEntity>
{
    public void Configure(EntityTypeBuilder<CreditCardEntity> builder)
    {
        builder.HasKey(creditCard => creditCard.Id);

        builder
            .Property(creditCard => creditCard.Id)
            .IsRequired();

        builder
            .Property(creditCard => creditCard.NumberCard)
            .HasMaxLength(16)
            .IsRequired();

        builder
            .Property(creditCard => creditCard.NameOnCreditCard)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(creditCard => creditCard.Validate)
            .HasMaxLength(7)
            .IsRequired();

        builder
            .Property(creditCard => creditCard.Cvc)
            .HasMaxLength(3)
            .IsRequired();
        
        builder
            .HasOne(creditCard => creditCard.Person)
            .WithMany(creditCard => creditCard.CreditCards)
            .HasForeignKey(creditCard => creditCard.PersonId);
    }
}
