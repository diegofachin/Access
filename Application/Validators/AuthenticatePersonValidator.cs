using Application.Handlers.AuthenticatePerson;
using Application.Handlers.RegisterPerson;
using Domain.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators;

[ExcludeFromCodeCoverage]
public class AuthenticatePersonValidator : AbstractValidator<AuthenticatePersonRequestDto>
{
    public AuthenticatePersonValidator()
    {
        RuleFor(request => request.Cpf)
           .NotEmpty()
           .Must(CpfValidator.Validate);

        RuleFor(request => request.Password)
            .NotEmpty();
    }
}
