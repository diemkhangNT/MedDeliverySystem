using Pharmacy.Application.Medication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Medication.Handlers
{
    public interface IMessageHandler
    {
        public Task UpsertMedicationAsync(UpsertMedicationDto dto);
    }
}
