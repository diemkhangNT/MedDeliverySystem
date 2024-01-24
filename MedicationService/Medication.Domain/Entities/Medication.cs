using Medication.Domain.DomainCommands;
using Medication.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Domain.Entities
{
    public class Medication : EntityBase
    {
        public Medication()
        {
        }

        public Medication(UpsertMedicationDomandCommand command)
        {
            Validate(command.Price, command.MedicineName);
            Name = command.MedicineName;
            Price = command.Price;
            CreatedUtc = ModifiedUtc = DateTime.UtcNow.ToLocalTime();
            CreatedBy = ModifiedBy = "Diem Khang";
        }

        private void Validate(decimal price, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidArgumentException(nameof(name), "cannot null");
            }
            if (!string.IsNullOrEmpty(name) && name.Length > 50)
            {
                throw new InvalidArgumentException(nameof(name), "cannot greater than 100 letters");
            }
            if(price <= 0)
            {
                throw new InvalidArgumentException(nameof(price), "cannot less than or equal to 0");
            }
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public void Update(UpsertMedicationDomandCommand command)
        {
            Name = command.MedicineName;
            Price = command.Price;
            ModifiedBy = "Khang Nguyen";
            ModifiedUtc = DateTime.UtcNow.ToLocalTime();
        }
    }
}
