﻿using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Application.Filters;

[ExcludeFromCodeCoverage]
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is null)
        {
            return;
        }

        if (context.Exception is ValidationException)
        {
            context.Result = new ObjectResult((context.Exception as ValidationException).Errors)
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
            };

            return;
        }

        if (context.Exception is DomainException)
        {
            context.Result = new ObjectResult(new { Error = (context.Exception as DomainException).Message })
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
            };

            return;
        }
    }
}