using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface IPharmacyRepository : ITransaction
    {
        public Task<IEnumerable<Entities.Pharmacy>> GetAllOfPharmacies(ExpressionModel<Entities.Pharmacy> expressionModel, bool? isTrackingForUpdate = true);
    }
}
