using Pharmacy.Domain.DomainCommands;
using Pharmacy.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class Pharmacy : EntityBase
    {
        private Pharmacy()
        {
        }

        public Pharmacy(CreatePharmacyDomainCommand command)
        {
            Id = command.PharmacyId;
            Name = command.PharmacyName;
            Contact = command.Contact;
            Address = command.Address;
            CreatedBy = ModifiedBy = "Diem Khang";
            CreatedUtc = ModifiedUtc = DateTime.UtcNow.ToLocalTime();
            IsDeleted = false;
        }

        public string Name { get; private set; }
        public Contact Contact { get; private set; }
        public Address Address { get; private set; }
    }
}
