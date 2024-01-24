using MedicineOrder.Application.Exceptions;
using MedicineOrder.Application.Medicine.Dtos;
using MedicineOrder.Application.Pharmacy.Dtos;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Pharmacy.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMedicineRepository _medicationRepository;

        public MessageHandler(IPharmacyRepository pharmacyRepository, IMedicineRepository medicineRepository)
        {
            _pharmacyRepository = pharmacyRepository;
            _medicationRepository = medicineRepository;
        }

        public async Task UpdateMedicationAsync(UpsertMedicationDto dto)
        {
            ExpressionModel<Domain.Entities.Medicine> predicate = new(medicine => medicine.Id == dto.Id || medicine.Name == dto.Name);
            var medication = await _medicationRepository.GetSingleMedication(predicate);

            var createMedicationDomandCommand = dto.ConvertMedicineDtoToCreateMedicineDomainCommand();

            if (medication == null)
            {
                medication = new Domain.Entities.Medicine(createMedicationDomandCommand);
                await _medicationRepository.InsertMedication(medication);
            }
            else
            {
                medication.Update(createMedicationDomandCommand);
                _medicationRepository.UpdateMedication(medication);
            }
            await _medicationRepository.UnitOfWork.SaveEntityChangesAsync();
        }

        public async Task UpdatePharmacyAsync(UpsertPharmacyDto dto)
        {
            ExpressionModel<Domain.Entities.Pharmacy> predicate = new(pharmacy => pharmacy.Id == dto.PharmacyId
                                                                    || pharmacy.Name == dto.PharmacyName);

            var pharmacy = await _pharmacyRepository.GetSinglePharmacy(predicate);

            var upsertPharmacyDomainCommand = dto.ConvertToUpsertPharmacyDomainCommand();

            if (pharmacy == null)
            {
                pharmacy = new Domain.Entities.Pharmacy(upsertPharmacyDomainCommand);
                await _pharmacyRepository.InsertPharmacy(pharmacy);
            }
            else
            {
                pharmacy.Update(upsertPharmacyDomainCommand);
                _pharmacyRepository.UpdatePharmacy(pharmacy);
            }
            await _pharmacyRepository.UnitOfWork.SaveEntityChangesAsync();
        }
    }
}
