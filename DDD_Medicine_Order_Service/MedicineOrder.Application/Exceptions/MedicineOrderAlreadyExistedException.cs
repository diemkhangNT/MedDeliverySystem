using MedicineOrder.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Exceptions
{
    public class MedicineOrderAlreadyExistedException : ApplicationException
    {
        public MedicineOrderAlreadyExistedException(int medicineOrderId) : 
            base($"Medicine Order {medicineOrderId} is already existed", HttpStatusCode.BadRequest)
        {
        }
    }
}
