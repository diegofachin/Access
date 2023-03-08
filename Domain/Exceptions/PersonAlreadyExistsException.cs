using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class PersonAlreadyExistsException : DomainException
{
    public PersonAlreadyExistsException() : base("Person already exists with this CPF.") { }
}
