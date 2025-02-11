﻿using Application.Behaviors;
using Application.Handlers.AddCreditCard;
using Application.Handlers.AuthenticatePerson;
using Application.Handlers.RegisterPerson;
using Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Application;

[ExcludeFromCodeCoverage]
public static class IoC
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidation();

        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IValidator<RegisterPersonRequestDto>, RegisterPersonValidator>();
        services.AddScoped<IValidator<AuthenticatePersonRequestDto>, AuthenticatePersonValidator>();
        services.AddScoped<IValidator<AddCreditCardRequestDto>, AddCreditCardValidator>();

        return services;
    }


}
