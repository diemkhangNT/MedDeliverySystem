using MedicineOrder.Application.Medicine.Dtos;
using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Application.Pharmacy.Dtos;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Pharmacy.Handlers
{
    public interface IMessageHandler
    {
        Task UpdatePharmacyAsync(UpsertPharmacyDto dto);
        Task UpdateMedicationAsync(UpsertMedicationDto dto);
    }
}
