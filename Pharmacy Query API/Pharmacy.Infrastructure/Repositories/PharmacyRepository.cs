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
    public class PharmacyRepository : BaseRepository<PharmacyDbContext, Domain.Entities.Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(PharmacyDbContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Domain.Entities.Pharmacy>> GetAllOfPharmacies(ExpressionModel<Domain.Entities.Pharmacy> expressionModel, bool? isTrackingForUpdate = true)
        {
            return await GetAllOfItems(expressionModel, isTrackingForUpdate);
        }
    }
}
