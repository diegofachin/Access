using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.RegisterPerson;

public class RegisterPersonResponseDto 
{
    public Guid PersonId { get; set; }
}
