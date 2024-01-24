using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Exceptions
{
    public class InvalidArgumentException : DomainException
    {
        public string Argument { get; private set; }
        public InvalidArgumentException(string argument, string message) : base(ExceptionCode.InvalidArgument, argument + " " + message)
        {
            Argument = argument;
        }
    }
}
