using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Interfaces
{
    public interface IMedicineRepository: ITransaction
    {
        public Task<IEnumerable<Medicine>> GetListMedicines(ExpressionModel<Medicine> expressionModel, bool? isTrackingForUpdate = true);
        public Task InsertMedication(Medicine medication);
        public void UpdateMedication(Medicine medication);
        public Task<Medicine> GetSingleMedication(ExpressionModel<Medicine> expressionModel, bool? isTrackingForUpdate = true);
    }
}
