using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Handlers.AuthenticatePerson;

[ExcludeFromCodeCoverage]
public class AuthenticatePersonRequestDto : IRequest<bool?>
{
    public string Cpf { get; set; }

    public string Password { get; set; }
}