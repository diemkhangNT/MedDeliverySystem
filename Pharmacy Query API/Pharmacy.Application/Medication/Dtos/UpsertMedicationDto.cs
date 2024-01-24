using Pharmacy.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Medication.Dtos
{
    public class UpsertMedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Source { get; set; }
        public CreateMedicineDomainCommand ConvertMedicineDtoToCreateMedicineDomainCommand()
        {
            return new CreateMedicineDomainCommand()
            {
                MedicineId = Id,
                MedicineName = Name,
                Price = Price,
            };
        }
    }
}
