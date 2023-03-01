using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Application.Enums;

namespace Infra.Mapping;

[ExcludeFromCodeCoverage]
public class PersonMap : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .Property(user => user.Id)
            .IsRequired();

        builder
            .Property(user => user.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(user => user.DateOfBirth)
            .HasColumnType("Date")
            .IsRequired();

        builder
            .Property(user => user.Gender)
            .HasConversion(new EnumToStringConverter<Gender>());

        builder
            .Property(user => user.Address)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(user => user.Gender)
            .HasMaxLength(5)
            .IsRequired();

        builder
            .Property(user => user.Complement)
            .HasMaxLength(200);

        builder
            .Property(user => user.Password)
            .HasMaxLength(200)
            .IsRequired();
    }
}
