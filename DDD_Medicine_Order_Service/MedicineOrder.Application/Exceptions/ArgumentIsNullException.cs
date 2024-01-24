using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Exceptions
{
    public class ArgumentIsNullException : ApplicationException
    {
        public ArgumentIsNullException(string argument) : 
            base($"{argument} cannot be null", HttpStatusCode.BadRequest)
        {
        }
    }
}
