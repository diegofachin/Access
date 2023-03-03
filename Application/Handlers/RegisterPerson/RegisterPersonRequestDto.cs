using Application.Enums;
using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.Handlers.RegisterPerson;

[ExcludeFromCodeCoverage]
public class RegisterPersonRequestDto : IRequest<RegisterPersonResponseDto>
{
    public string Name { get; set; }

    public string Cpf { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string Address { get; set; }

    public int AddressNumber { get; set; }

    public string Complement { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}
