using Pharmacy.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.DomainCommands
{
    public class CreatePharmacyDomainCommand
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
