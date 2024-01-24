using MedicineOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Exceptions
{
    public class MedicineOrdersIsNullException : DomainException
    {
        public MedicineOrdersIsNullException() : 
            base(ExceptionCode.InvalidMedicineOrder, "Medicine Order detail cannot be null", HttpStatusCode.BadRequest)
        {
        }
    }
}
