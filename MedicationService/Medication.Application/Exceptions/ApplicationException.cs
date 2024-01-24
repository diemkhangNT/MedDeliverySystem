using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        private static string PrependMessage = "Application Exception: ";
        public ApplicationException(string errorDescription) : base(PrependMessage + errorDescription)
        {
            ErrorDescription = errorDescription;
        }
        public ApplicationException(string errorDescription, HttpStatusCode httpStatusCode) : base(PrependMessage + errorDescription)
        {
            StatusCode = httpStatusCode;
            ErrorDescription = errorDescription;
        }
        public string ErrorDescription { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}
