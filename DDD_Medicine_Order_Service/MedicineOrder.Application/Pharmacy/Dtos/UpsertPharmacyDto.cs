using MedicineOrder.Application.Common;
using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Pharmacy.Dtos
{
    public record UpsertPharmacyDto
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }

        public UpsertPharmacyDomainCommand ConvertToUpsertPharmacyDomainCommand() => new()
        {
            Address = new Domain.ValueObjects.Address(Address?.Address1,
                                                      Address?.Address2,
                                                      Address?.Suburb,
                                                      Address?.State,
                                                      Address?.PostCode,
                                                      Address.PharmacyId),

            Contact = new Domain.ValueObjects.Contact(Contact?.Email, Contact?.PhoneNumber, Contact.PharmacyId),
            PharmacyName = this.PharmacyName,
            Id = this.PharmacyId
        };
    }
}
