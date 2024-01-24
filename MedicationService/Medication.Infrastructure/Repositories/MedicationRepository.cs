using Medication.Domain.Extensions;
using Medication.Domain.Interfaces;
using Medication.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Infrastructure.Repositories
{
    public class MedicationRepository : BaseRepository<MedicationDBContext, Domain.Entities.Medication>, IMedicationRepository
    {
        public MedicationRepository(MedicationDBContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.Entities.Medication> GetSingleMedication(ExpressionModel<Domain.Entities.Medication> expressionModel, bool? isTrackingForUpdate = true)
        {
            return await GetSingleItem(expressionModel, isTrackingForUpdate);
        }

        public async Task InsertMedication(Domain.Entities.Medication medication)
        {
            await InsertItem(medication);
        }

        public void UpdateMedication(Domain.Entities.Medication medication)
        {
            UpdateItem(medication);
        }
    }
}
