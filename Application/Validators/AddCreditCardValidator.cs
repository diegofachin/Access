﻿using Application.Handlers.AddCreditCard;
using Application.Handlers.AuthenticatePerson;
using Domain.Validators;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Application.Validators;

[ExcludeFromCodeCoverage]
public class AddCreditCardValidator : AbstractValidator<AddCreditCardRequestDto>
{
    public AddCreditCardValidator()
    {
        RuleFor(request => request.PersonId)
           .NotEmpty();

        RuleFor(request => request.NameOnCreditCard)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(request => request.NumberCard)
            .NotEmpty()
            .Must(NumberCardValidator.Validate);

        RuleFor(request => request.Cvc)
            .NotEmpty();

        RuleFor(request => request.Validate)
            .NotEmpty();
    }
}
