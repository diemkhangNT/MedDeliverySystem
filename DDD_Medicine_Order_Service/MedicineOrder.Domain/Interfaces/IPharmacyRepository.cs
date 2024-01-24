using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Interfaces
{
    public interface IPharmacyRepository : ITransaction
    {
        public Task InsertPharmacy(Pharmacy pharmacy);
        public void UpdatePharmacy(Pharmacy pharmacy);
        public Task<Pharmacy> GetSinglePharmacy(ExpressionModel<Pharmacy> expressionModel, bool? isTrackingForUpdate = true);
        public Task<IEnumerable<Pharmacy>> GetAllOfPharmacy(ExpressionModel<Pharmacy> expressionModel, bool? isTrackingForUpdate = true);
    }
}
