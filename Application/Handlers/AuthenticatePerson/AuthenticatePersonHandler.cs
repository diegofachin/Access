using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticatePerson;

public class AuthenticatePersonHandler : IRequestHandler<AuthenticatePersonRequestDto, AuthenticatePersonResponseDto>
{
    public Task<AuthenticatePersonResponseDto> Handle(AuthenticatePersonRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
