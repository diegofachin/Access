using Application.Handlers.RegisterPerson;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AddCreditCard;

[ExcludeFromCodeCoverage]
public class AddCreditCardRequestDto : IRequest<AddCreditCardResponseDto>
{
    public Guid PersonId { get; set; }
    
    public string NumberCard { get; set; }

    public string NameOnCreditCard { get; set; }

    public string Validate { get; set; }

    public string Cvc { get; set; }
}
