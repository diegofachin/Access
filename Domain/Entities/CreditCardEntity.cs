using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class CreditCardEntity : BaseEntity
{
    public Guid PersonId { get; set; }

    public string NumberCard { get; set; }

    public string NameOnCreditCard { get; set; }

    public string Validate { get; set; }

    public string Cvc { get; set; }

    public virtual PersonEntity Person { get; set; }
}
