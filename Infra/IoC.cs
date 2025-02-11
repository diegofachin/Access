﻿using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra;

[ExcludeFromCodeCoverage]
public static class IoC
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository();

        services.AddDbContext<PersonDbContext>(opt => opt
            .UseSqlServer(configuration.GetConnectionString("PersonConnection"), option => option.EnableRetryOnFailure()));        

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<ICreditCardRepository, CreditCardRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}