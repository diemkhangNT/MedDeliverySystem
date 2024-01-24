using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Medicine.Dtos
{
    public record MedicineDto
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public decimal Price { get; set; }
        public MedicineDto(Domain.Entities.Medicine medicine)
        {
            Id = medicine.Id;
            MedicineName = medicine.Name;
            Price = medicine.Price;
        }

        public CreateMedicineDomainCommand ConvertMedicineDtoToCreateMedicineDomainCommand()
        {
            return new CreateMedicineDomainCommand()
            {
                MedicineId = Id,
                MedicineName = MedicineName,
                Price = Price,
            };
        }
    }
}
