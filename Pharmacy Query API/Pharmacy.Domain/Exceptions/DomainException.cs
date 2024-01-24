using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public ExceptionCode Code { get; private set; }
        private static string DomainExceptionPrepend = "Domain Exception: ";
        public HttpStatusCode StatusCode { get; private set; }

        public DomainException(ExceptionCode code, string message) : base(DomainExceptionPrepend + message)
        {
            Code = code;
        }
        
        public DomainException(ExceptionCode code, string message, HttpStatusCode statusCode) : base(DomainExceptionPrepend + message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
    public enum ExceptionCode
    {
        InvalidArgument = 1001,
        UnknownException = 9999
    }
}
