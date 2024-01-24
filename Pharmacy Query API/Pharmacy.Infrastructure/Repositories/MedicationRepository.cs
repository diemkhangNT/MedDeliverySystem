using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Extensions;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class MedicationRepository : BaseRepository<PharmacyDbContext, Medication> ,IMedicationRepository
    {
        public MedicationRepository(PharmacyDbContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Medication> GetSingleMedication(ExpressionModel<Medication> expressionModel, bool? isTrackingForUpdate = true)
        => await GetSingleItem(expressionModel, isTrackingForUpdate);

        public async Task InsertMedication(Medication medication)
        => await InsertItem(medication);

        public void UpdateMedication(Medication medication)
        => UpdateItem(medication);
    }
}
