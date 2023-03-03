using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.RegisterPerson;

[ExcludeFromCodeCoverage]
public class RegisterPersonResponseDto 
{
    public string Cpf { get; set; }
}
