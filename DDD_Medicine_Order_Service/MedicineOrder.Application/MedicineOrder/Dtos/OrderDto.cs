using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Dtos
{
    public record OrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
        public int PharmacyID { get; set; }
        public IList<MedicineOrderDto> MedicineOrderDetails { get; set; }

        public OrderDto(Domain.Entities.Order medicineOrder)
        {
            Id = medicineOrder.Id;
            TotalPrice = medicineOrder.TotalPrice;
            OrderStatus = medicineOrder.OrderStatus;
            PharmacyID = medicineOrder.PharmacyID;
            MedicineOrderDetails = medicineOrder.MedicineOrders.Select(medOrder => new MedicineOrderDto(medOrder)).ToList();
        }
    }
}
