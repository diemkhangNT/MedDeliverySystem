using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using MedicineOrder.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.Repositories
{
    public class MedicineOrderRepository : BaseRepository<MedicineOrderDbContext, Order>, IMedicineOrderRepository
    {
        public MedicineOrderRepository(MedicineOrderDbContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Order>> GetAllOfMedicineOrders(ExpressionModel<Order> expressionModel, bool? isTrackingForUpdate = true)
        {
            return await GetAllOfItems(expressionModel, isTrackingForUpdate);
        }

        public async Task<Order> GetSingleMedicineOrder(ExpressionModel<Order> expressionModel, bool? isTrackingForUpdate = true)
        {
            return await GetSingleItem(expressionModel, isTrackingForUpdate);
        }

        public async Task InsertMedicineOrder(Order order)
        {
            await InsertItem(order);
        }

        public void UpdateMedicineOrder(Order order)
        {
            UpdateItem(order);
        }
    }
}
