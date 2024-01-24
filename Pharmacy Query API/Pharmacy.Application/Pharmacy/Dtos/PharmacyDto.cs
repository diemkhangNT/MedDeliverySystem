using Pharmacy.Application.Common;
using Pharmacy.Domain.DomainCommands;
using Pharmacy.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Pharmacy.Dtos
{
    public record PharmacyDto
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }
        public string Source { get; set; }

        public PharmacyDto(Domain.Entities.Pharmacy pharmacy)
        {
            PharmacyId = pharmacy.Id;
            PharmacyName = pharmacy.Name;
            Address = new(pharmacy.Address.Address1,
                        pharmacy.Address.Address2,
                        pharmacy.Address.Suburb,
                        pharmacy.Address.State,
                        pharmacy.Address.PostCode);
            Contact = new(pharmacy.Contact.Email,
                        pharmacy.Contact.NumberPhone);
        }

    }
}
