using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Exceptions
{
    public class MedicineNotFoundException : DomainException
    {
        public MedicineNotFoundException(int medId) 
            : base(ExceptionCode.MedicineIsNotExisted, $"Medicine {medId} is not found!")
        {
        }
    }
}
