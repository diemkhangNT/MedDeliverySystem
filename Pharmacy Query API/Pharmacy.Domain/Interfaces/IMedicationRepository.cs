using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface IMedicationRepository : ITransaction
    {
        public Task InsertMedication(Medication medication);
        public void UpdateMedication(Medication medication);
        public Task<Medication> GetSingleMedication(ExpressionModel<Medication> expressionModel, bool? isTrackingForUpdate = true);
    }
}
