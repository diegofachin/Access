using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.RegisterPerson;

public class RegisterPersonHandler : IRequestHandler<RegisterPersonRequestDto, RegisterPersonResponseDto>
{
    public Task<RegisterPersonResponseDto> Handle(RegisterPersonRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
