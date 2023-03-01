using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Application.Filters;
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
        }
    }
}