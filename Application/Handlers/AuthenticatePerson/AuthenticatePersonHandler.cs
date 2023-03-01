using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticatePerson;

public class AuthenticatePersonHandler : IRequestHandler<AuthenticatePersonRequestDto, bool?>
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticatePersonHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool?> Handle(AuthenticatePersonRequestDto request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PersonRepository.AuthenticatePerson(request.Cpf, request.Password);
    }
}
