using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Dtos
{
    public record UpsertOrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
        public int PharmacyID { get; set; }
        public IList<UpsertMedicineOrderDto> OrderDetails { get; set; }

        public InsertOrderDomainCommand ConvertToOrderDomainCommand()
        {
            return new InsertOrderDomainCommand()
            {
                PharmacyID = PharmacyID,
                OrderStatus = OrderStatus,
                TotalPrice = TotalPrice,
                MedicineOrderDetails = OrderDetails.Select(detail => detail.ConvertToMedicineOrderDomainCommand()).ToList()
            };
        }
    }
}
