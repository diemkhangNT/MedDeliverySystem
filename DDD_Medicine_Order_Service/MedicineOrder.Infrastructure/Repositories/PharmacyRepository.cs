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
    public class PharmacyRepository : BaseRepository<MedicineOrderDbContext, Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(MedicineOrderDbContext context) : base(context)
        {
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Pharmacy>> GetAllOfPharmacy(ExpressionModel<Pharmacy> expressionModel, bool? isTrackingForUpdate = true)
        => await GetAllOfItems(expressionModel, isTrackingForUpdate);

        public async Task<Pharmacy> GetSinglePharmacy(ExpressionModel<Pharmacy> expressionModel, bool? isTrackingForUpdate = true)
        => await GetSingleItem(expressionModel, isTrackingForUpdate);

        public async Task InsertPharmacy(Pharmacy pharmacy)
        => await InsertItem(pharmacy);

        public void UpdatePharmacy(Pharmacy pharmacy)
        =>  UpdateItem(pharmacy);
    }
}
