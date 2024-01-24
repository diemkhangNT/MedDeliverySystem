using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Entities
{
    public class Pharmacy : EntityBase
    {
        public Pharmacy()
        {
        }

        public Pharmacy(UpsertPharmacyDomainCommand command)
        {
            Id = command.Id;
            Name = command.PharmacyName;
            Address = command.Address;
            Contact = command.Contact;
            ModifiedBy = CreatedBy = "Khang Nguyen";
            ModifiedUtc = CreatedUtc = DateTime.UtcNow.ToLocalTime();
        }

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public Contact Contact { get; private set; }
        public ICollection<Order> Orders { get; private set; }

        public void Update(UpsertPharmacyDomainCommand command)
        {
            Name = command.PharmacyName;
            Address = new(
                command.Address?.Address1,
                command.Address?.Address2,
                command.Address?.Suburb,
                command.Address?.State,
                command.Address?.PostCode,
                command.Address.PharmacyId);
            Contact = new(
                command.Contact.Email,
                command.Contact.NumberPhone,
                command.Contact.PharmacyId);
            ModifiedBy = "Diem Khang NT";
            ModifiedUtc = DateTime.UtcNow.ToLocalTime();
        }
    }
}
