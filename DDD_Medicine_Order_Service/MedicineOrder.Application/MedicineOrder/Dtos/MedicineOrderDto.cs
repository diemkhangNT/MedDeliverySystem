using MedicineOrder.Application.Medicine.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Dtos
{
    public record MedicineOrderDto
    {
        public int OrderID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public MedicineDto MedicineDetail { get; set; }

        public MedicineOrderDto(Domain.Entities.MedicineOrder entity)
        {
            OrderID = entity.OrderID;
            MedicineID = entity.MedicineID;
            Quantity = entity.Quantity;
        }

        public MedicineOrderDto(MedicineDto dto)
        {
            MedicineDetail = dto;
        }
    }
}
