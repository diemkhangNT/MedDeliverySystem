using Medication.Application.Medication.Dtos;
using Medication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Medication.Handlers
{
    public class UpsertMedicationHandler : IUpsertMedicationHandler
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IBroadcastMedicationHandler _broadcastMedicationHandler;

        public UpsertMedicationHandler(IMedicationRepository medicationRepository, IBroadcastMedicationHandler broadcastMedicationHandler)
        {
            _medicationRepository = medicationRepository;
            _broadcastMedicationHandler = broadcastMedicationHandler;
        }
        public async Task UpsertMedicationAsync(UpsertMedicationDto dto)
        {
            var medication = await _medicationRepository.GetSingleMedication(new(medication => medication.Id == dto.Id));
            
            var upsertMedicationDomandCommand = dto.ConvertToUpsertMedicationDomainCommand();

            if (medication == null)
            {
                medication = new Domain.Entities.Medication(upsertMedicationDomandCommand);
                await _medicationRepository.InsertMedication(medication);
            }
            else
            {
                medication.Update(upsertMedicationDomandCommand);
                _medicationRepository.UpdateMedication(medication);
            }
            await _medicationRepository.UnitOfWork.SaveEntityChangesAsync();

            //send message
            var broadcastMedication = dto.ConvertMedicationToBroadcastMedicationDto(medication);
            await _broadcastMedicationHandler.BroadcastMedicationAsync(broadcastMedication);
        }
    }
}
