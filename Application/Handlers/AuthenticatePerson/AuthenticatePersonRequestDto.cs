using MediatR;

namespace Application.Handlers.AuthenticatePerson;

public class AuthenticatePersonRequestDto : IRequest<bool?>
{
    public string Cpf { get; set; }

    public string Password { get; set; }
}