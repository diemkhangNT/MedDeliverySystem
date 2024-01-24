using MedicineOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.DomainCommands
{
    public class InsertMedicineOrderDomainCommand
    {
        public int OrderID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
    }
}
