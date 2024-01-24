using MedicineOrder.Application.Medicine.Dtos;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Medicine.Handlers
{
    public class GetListMedicinesHandler : IGetListMedicinesHandler
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetListMedicinesHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<List<MedicineDto>> GetListMedicinesAsync()
        {
            var listMedicines = await _medicineRepository.GetListMedicines(new(medicine => !medicine.IsDeleted));
            var response = listMedicines.Select(med => new MedicineDto(med)).ToList();
            return response;
        }

        public async Task<List<MedicineDto>> GetSingleMedicineAsync(GetMedicineDto dto)
        {
            ExpressionModel<Domain.Entities.Medicine> predicate = new(medicine => medicine.Id == dto.MedicineId 
                                                                    && !medicine.IsDeleted);
            var medicine = await _medicineRepository.GetListMedicines(predicate);
            var response = medicine.Select(med => new MedicineDto(med)).ToList();
            return response;
        }
    }
}
