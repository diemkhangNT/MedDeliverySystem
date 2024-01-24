using Medication.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Application.Medication.Dtos
{
    public record UpsertMedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public decimal Price { get; set;}

        public UpsertMedicationDomandCommand ConvertToUpsertMedicationDomainCommand()
            => new()
            {
                MedicineName = Name,
                Price = Price
            };

        public BroadcastMedicationDto ConvertMedicationToBroadcastMedicationDto(Domain.Entities.Medication medication)
            => new()
            {
                Id = medication.Id,
                Name = medication.Name,
                Price = medication.Price,
                CreatedUtc = medication.CreatedUtc,
                ModifiedUtc = medication.ModifiedUtc,
                CreatedBy = medication.CreatedBy,
                ModifiedBy = medication.ModifiedBy,
                IsDeleted = medication.IsDeleted,
                Source = "Medication"
            };
    }
}
