using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Interfaces
{
    public interface IMedicineOrderRepository : ITransaction
    {
        public Task InsertMedicineOrder(Order order);
        public void UpdateMedicineOrder(Order order);
        public Task<Order> GetSingleMedicineOrder(ExpressionModel<Order> expressionModel, bool? isTrackingForUpdate = true);
        public Task<IEnumerable<Order>> GetAllOfMedicineOrders(ExpressionModel<Order> expressionModel, bool? isTrackingForUpdate = true);
    }
}
