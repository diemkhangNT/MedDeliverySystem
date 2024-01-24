using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.DomainCommands
{
    public class InsertOrderDomainCommand
    {
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
        public int PharmacyID { get; set; }
        public ICollection<InsertMedicineOrderDomainCommand> MedicineOrderDetails { get; set; }
    }
}
