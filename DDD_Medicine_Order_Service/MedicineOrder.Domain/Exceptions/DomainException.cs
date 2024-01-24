using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public ExceptionCode Code { get; set; }
        private static string DomainExceptionPrepend = "Domain Exception: "; 
        public DomainException(ExceptionCode code, string message): base(DomainExceptionPrepend +  message)
        {
            Code = code;
        }

        public DomainException(ExceptionCode code, string message, HttpStatusCode statusCode): base(DomainExceptionPrepend + message)
        {
            Code = code;
            StatusCode = statusCode;
        }

        //public string ErrorDescription { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }

    public enum ExceptionCode
    {
        InvalidArgument = 1001,
        InvalidMedicineOrder = 1002,
        MedicineOrderAlreadyExisted= 1003,
        MedicineIsNotExisted= 1004,
        UnknownException = 9999
    }
}
