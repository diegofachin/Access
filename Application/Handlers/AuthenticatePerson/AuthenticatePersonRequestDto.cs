using MediatR;

namespace Application.Handlers.AuthenticatePerson;

public class AuthenticatePersonRequestDto : IRequest<AuthenticatePersonResponseDto>
{
    public string Cpf { get; set; }

    public string Password { get; set; }
}