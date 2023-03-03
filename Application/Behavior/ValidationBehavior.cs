using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviors;

[ExcludeFromCodeCoverage]
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _provider;

    public ValidationBehavior(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator = _provider.GetService<IValidator<TRequest>>();
        if (validator is null)
        {
            return await next();
        }

        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidationException("Error", result.Errors);
        }

        return await next();
    }
}
