using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Exceptions
{
    public class ArgumentIsNullException : ApplicationException
    {
        public ArgumentIsNullException(string argument) : base(nameof(argument) + " cannot be null", HttpStatusCode.BadRequest)
        {
        }
    }
}
