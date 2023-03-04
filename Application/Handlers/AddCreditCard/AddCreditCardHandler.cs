using Application.Handlers.RegisterPerson;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.AddCreditCard;

public class AddCreditCardHandler : IRequestHandler<AddCreditCardRequestDto, AddCreditCardResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCreditCardHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AddCreditCardResponseDto> Handle(AddCreditCardRequestDto request, CancellationToken cancellationToken)
    {
        CreditCardEntity creditCard = new()
        {
            PersonId = request.PersonId,
            NumberCard = request.NumberCard,
            NameOnCreditCard = request.NameOnCreditCard,
            Cvc = request.Cvc,
            Validate = request.Validate
        };

        await _unitOfWork.CreditCardRepository.Add(creditCard);
        _unitOfWork.Commit();

        return new AddCreditCardResponseDto()
        {
            Id = creditCard.Id
        };
    }
}
