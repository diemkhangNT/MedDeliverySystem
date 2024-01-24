using Medication.Application.Medication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Medication.Handlers
{
    public interface IBroadcastMedicationHandler
    {
        Task BroadcastMedicationAsync(BroadcastMedicationDto dto);
    }
}
