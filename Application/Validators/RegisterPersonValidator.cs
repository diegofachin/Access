using Application.Handlers.RegisterPerson;
using Domain.Validators;
using FluentValidation;
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

        RuleFor(request => request.Password)
            .NotEmpty()
            .Must(PasswordValidator.Validate);

        RuleFor(request => request)
            .NotEmpty()
            .Must(request => request.Password == request.ConfirmPassword);
    }
}
