using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Medicine.Dtos
{
    public record UpsertMedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

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
