using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Exceptions
{
    public class MedicineOrderNotFoundException : ApplicationException
    {
        public MedicineOrderNotFoundException(int medicineId)
            : base($"Medicine Order with Id {medicineId} does not exist.", HttpStatusCode.BadRequest)
        { }
    }
}
