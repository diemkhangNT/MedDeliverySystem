using Medication.Application.Medication.Dtos;
using Medication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Medication.Handlers
{
    public interface IUpsertMedicationHandler
    {
        Task UpsertMedicationAsync(UpsertMedicationDto dto);
    }
}
