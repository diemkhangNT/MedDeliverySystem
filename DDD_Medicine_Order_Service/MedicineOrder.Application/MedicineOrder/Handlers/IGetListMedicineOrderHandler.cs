using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public interface IGetListMedicineOrderHandler
    {
        Task<List<OrderDto>> GetSingleMedicineOrder(GetListOrderDto dto);
        Task<List<OrderDto>> GetListMedicineOrders();
        Task<List<OrderDto>> GetListMedicineOrdersIsDeleted();
        Task<List<OrderDto>> GetListMedicineOrdersByPharmacyId(GetListOrderByPharmacyIdDto dto);
    }
}
