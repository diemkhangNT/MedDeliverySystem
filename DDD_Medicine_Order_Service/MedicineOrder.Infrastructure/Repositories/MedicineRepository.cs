using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using MedicineOrder.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.Repositories
{
    public class MedicineRepository : BaseRepository<MedicineOrderDbContext, Medicine>, IMedicineRepository
    {
        public MedicineRepository(MedicineOrderDbContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Medicine>> GetListMedicines(ExpressionModel<Medicine> expressionModel, bool? isTrackingForUpdate = true)
        => await GetAllOfItems(expressionModel, isTrackingForUpdate);

        public async Task<Medicine> GetSingleMedication(ExpressionModel<Medicine> expressionModel, bool? isTrackingForUpdate = true)
        {
            return await GetSingleItem(expressionModel, isTrackingForUpdate);
        }

        public async Task InsertMedication(Medicine medication)
        => await InsertItem(medication);

        public void UpdateMedication(Medicine medication)
        => UpdateItem(medication);
    }
}
