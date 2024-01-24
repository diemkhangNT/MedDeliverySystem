using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public ExceptionCode ExceptionCode { get; private set; }
        private static string DomainExceptionPrepend = "Domain Exception: ";
        public HttpStatusCode StatusCode { get; private set; }

        public DomainException(ExceptionCode exceptionCode, string message, HttpStatusCode statusCode) 
            : base (DomainExceptionPrepend + message + ", status code: " + statusCode)
        {
            ExceptionCode = exceptionCode;
            StatusCode = statusCode;
        }

        public DomainException(ExceptionCode exceptionCode, string message) : base(DomainExceptionPrepend + message)
        {
            ExceptionCode = exceptionCode;
        }
    }

    public enum ExceptionCode
    {
        InvalidArgument = 1001,
        UnknownException = 9999
    }
}
