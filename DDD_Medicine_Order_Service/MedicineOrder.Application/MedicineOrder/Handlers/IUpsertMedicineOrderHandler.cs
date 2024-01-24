using MedicineOrder.Application.MedicineOrder.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public interface IUpsertMedicineOrderHandler
    {
        public Task InsertOrderHandler(UpsertOrderDto dto);
        public Task DeleteMedicineOrderHandler(UpsertOrderDto dto);
    }
}
