using MedicineOrder.Application.Medicine.Dtos;
using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Medicine.Handlers
{
    public interface IGetListMedicinesHandler
    {
        Task<List<MedicineDto>> GetListMedicinesAsync();
        Task<List<MedicineDto>> GetSingleMedicineAsync(GetMedicineDto dto);
    }
}
