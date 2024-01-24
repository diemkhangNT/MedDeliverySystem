using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Entities
{
    public class MedicineOrder
    {
        public MedicineOrder()
        {
        }

        public MedicineOrder(InsertMedicineOrderDomainCommand command)
        {
            OrderID = command.OrderID;
            MedicineID = command.MedicineID;
            Quantity = command.Quantity;
        }

        public int OrderID { get; private set; }
        public int MedicineID { get; private set; }
        public int Quantity { get; private set; }
        public Order Order { get; private set; }
        public Medicine Medicine { get; private set; }
    }
}
