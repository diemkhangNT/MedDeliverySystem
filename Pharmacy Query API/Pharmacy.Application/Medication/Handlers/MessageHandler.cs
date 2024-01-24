using Pharmacy.Application.Medication.Dtos;
using Pharmacy.Domain.Extensions;
using Pharmacy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Medication.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IMedicationRepository _medicationRepository;

        public MessageHandler(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task UpsertMedicationAsync(UpsertMedicationDto dto)
        {
            ExpressionModel<Domain.Entities.Medication> predicate = new(medication => medication.Id == dto.Id || medication.Name == dto.Name);

            var medication = await _medicationRepository.GetSingleMedication(predicate);

            var createMedicineDomainCommand = dto.ConvertMedicineDtoToCreateMedicineDomainCommand();

            if (medication == null)
            {
                medication = new Domain.Entities.Medication(createMedicineDomainCommand);
                await _medicationRepository.InsertMedication(medication);
            }
            else
            {
                medication.Update(createMedicineDomainCommand);
                _medicationRepository.UpdateMedication(medication);
            }
            await _medicationRepository.UnitOfWork.SaveEntityChangesAsync();
        }
    }
}
