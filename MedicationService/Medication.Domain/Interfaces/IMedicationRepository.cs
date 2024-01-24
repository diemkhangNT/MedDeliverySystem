using Medication.Domain.Entities;
using Medication.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Domain.Interfaces
{
    public interface IMedicationRepository : ITransaction
    {
        public Task InsertMedication(Entities.Medication medication);
        public void UpdateMedication(Entities.Medication medication);
        public Task<Entities.Medication> GetSingleMedication(ExpressionModel<Entities.Medication> expressionModel, bool? isTrackingForUpdate = true);
    }
}
