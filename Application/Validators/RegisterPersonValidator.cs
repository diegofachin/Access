using Application.Handlers.RegisterPerson;
using Domain.Validators;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.Validators;

[ExcludeFromCodeCoverage]
public class RegisterPersonValidator : AbstractValidator<RegisterPersonRequestDto>
{
    public RegisterPersonValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(request => request.Cpf)
            .NotEmpty()
            .Must(CpfValidator.Validate);

        RuleFor(request => request.DateOfBirth)
            .NotEmpty()
            .Must(request => !request.Equals(default))
            .LessThan(request => DateTime.Now);

        RuleFor(request => request.Gender)
            .NotEmpty();

        RuleFor(request => request.Address)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(request => request.AddressNumber)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Must(PasswordValidator.Validate);

        RuleFor(request => request)
            .NotEmpty()
            .Must(request => request.Password == request.ConfirmPassword);
    }
}
