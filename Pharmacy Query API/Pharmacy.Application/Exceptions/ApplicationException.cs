using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        private static string PrependMessage = "Application Exception: ";
        public ApplicationException(string ErrorDescription) : base(PrependMessage + ErrorDescription) { }
        public ApplicationException(string errorDescription, HttpStatusCode httpStatusCode) : base(PrependMessage + errorDescription)
        {
            StatusCode = httpStatusCode;
        }
        public string ErrorDescription { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}
