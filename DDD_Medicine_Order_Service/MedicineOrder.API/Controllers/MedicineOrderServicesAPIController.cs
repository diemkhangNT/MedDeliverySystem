using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Application.MedicineOrder.Handlers;
using MedicineOrder.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineOrderServicesAPIController : ControllerBase
    {
        private readonly IGetListMedicineOrderHandler _getListmedicineOrder;
        private readonly IUpsertMedicineOrderHandler _upsertMedOrder;

        public MedicineOrderServicesAPIController(IGetListMedicineOrderHandler getListmedicineOrder, IUpsertMedicineOrderHandler upsertMedicineOrder)
        {
            _getListmedicineOrder = getListmedicineOrder;
            _upsertMedOrder = upsertMedicineOrder;
        }

        [HttpGet("/GetListMedicineOrders")]
        public async Task<List<OrderDto>> GetListOrders()
        {
            var orders = await _getListmedicineOrder.GetListMedicineOrders();
            return orders;
        }

        [HttpGet("/GetListMedicineOrdersIsDeleted")]
        public async Task<List<OrderDto>> GetListOrdersIsDeleted()
        {
            var orders = await _getListmedicineOrder.GetListMedicineOrdersIsDeleted();
            return orders;
        }

        [HttpGet("/GetMedicineOrderById/{Id}")]
        public async Task<List<OrderDto>> GetOrderById(int Id)
        {
            var medicineOrder = await _getListmedicineOrder.GetSingleMedicineOrder(new() { OrderId = Id });
            return medicineOrder;
        }

        [HttpGet("/GetMedicineOrderByPharmacyId/{pharmacyId}")]
        public async Task<List<OrderDto>> GetOrderByPharmacyId(int pharmacyId)
        {
            var medicineOrder = await _getListmedicineOrder.GetListMedicineOrdersByPharmacyId(new() { PharmacyId = pharmacyId });
            return medicineOrder;
        }

        [HttpPost("/AddMedicineOrder")]
        public async Task<IActionResult> AddMedicineOrder(UpsertOrderDto order)
        {
            await _upsertMedOrder.InsertOrderHandler(order);
            return Ok("Medicine Order is created and sent message!");
        }


        [HttpPut("/DeleteMedicineOrder/{Id}")]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            await _upsertMedOrder.DeleteMedicineOrderHandler(new() { Id = Id });
            return Ok("Medicine Order is deleted!");
        }
    }
}
