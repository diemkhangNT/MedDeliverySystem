using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Dtos
{
    public record UpsertMedicineOrderDto
    {
        
        public int OrderID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }

        public InsertMedicineOrderDomainCommand ConvertToMedicineOrderDomainCommand()
        {
            return new InsertMedicineOrderDomainCommand()
            {
                MedicineID = MedicineID,
                Quantity = Quantity,
                OrderID = OrderID
            };
        }
    }
}
