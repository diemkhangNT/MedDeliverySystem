using Pharmacy.Domain.DomainCommands;
using Pharmacy.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class Medication : EntityBase
    {
        public Medication()
        {
        }

        public Medication(CreateMedicineDomainCommand command)
        {
            Id = command.MedicineId;
            Name = command.MedicineName;
            Price = command.Price;
            ModifiedBy = CreatedBy = "Khang Nguyen";
            ModifiedUtc = CreatedUtc = DateTime.UtcNow.ToLocalTime();
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public void Update(CreateMedicineDomainCommand command)
        {
            Name = command.MedicineName;
            Price = command.Price;
            ModifiedBy = "Khang Nguyen";
            ModifiedUtc = DateTime.UtcNow.ToLocalTime();
        }
    }
}
