using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.RegisterPerson;

public class RegisterPersonHandler : IRequestHandler<RegisterPersonRequestDto, RegisterPersonResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPersonHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<RegisterPersonResponseDto> Handle(RegisterPersonRequestDto request, CancellationToken cancellationToken)
    {
        PersonEntity person = new()
        {
            Name = request.Name,
            Cpf = request.Cpf,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            AddressNumber = request.AddressNumber,
            Complement = request.Complement,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        await _unitOfWork.PersonRepository.Add(person);
        _unitOfWork.Commit();

        return new RegisterPersonResponseDto()
        {
            Cpf = person.Cpf
        };
    }
}
