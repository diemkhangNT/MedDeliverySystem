using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public class GetListMedicineOrderHandler : IGetListMedicineOrderHandler
    {
        private readonly IMedicineOrderRepository _medicineOrderRepository;

        public GetListMedicineOrderHandler(IMedicineOrderRepository medicineOrderRepository)
        {
            _medicineOrderRepository = medicineOrderRepository;
        }

        public async Task<List<OrderDto>> GetListMedicineOrders()
        {
            ExpressionModel<Domain.Entities.Order> predicate = new(medicine => medicine.Id != 0
                                                                && medicine.IsDeleted == false);
            var medicineOrderList = await _medicineOrderRepository.GetAllOfMedicineOrders(predicate);
            var respone = medicineOrderList.Select(medicineOrder => new OrderDto(medicineOrder)).ToList();

            return respone;
        }

        public async Task<List<OrderDto>> GetListMedicineOrdersByPharmacyId(GetListOrderByPharmacyIdDto dto)
        {
            ExpressionModel<Domain.Entities.Order> predicate = new(medicine => !medicine.IsDeleted
                                                            && medicine.PharmacyID == dto.PharmacyId);
            var medicineList = await _medicineOrderRepository.GetAllOfMedicineOrders(predicate);
            var respone = medicineList.Select(medicine => new OrderDto(medicine)).ToList();
            return respone;
        }

        public async Task<List<OrderDto>> GetListMedicineOrdersIsDeleted()
        {
            ExpressionModel<Domain.Entities.Order> predicate = new(medicine => medicine.Id != 0
                                                                && medicine.IsDeleted == true);
            var medicineList = await _medicineOrderRepository.GetAllOfMedicineOrders(predicate);
            var respone = medicineList.Select(medicine => new OrderDto(medicine)).ToList();
            return respone;
        }

        public async Task<List<OrderDto>> GetSingleMedicineOrder(GetListOrderDto dto)
        {
            var orderList = await _medicineOrderRepository.GetAllOfMedicineOrders(new(medicine => medicine.Id == dto.OrderId && medicine.IsDeleted == false));
            var response = orderList.Select(order => new OrderDto(order)).ToList();
            return response;
        }
    }
}
