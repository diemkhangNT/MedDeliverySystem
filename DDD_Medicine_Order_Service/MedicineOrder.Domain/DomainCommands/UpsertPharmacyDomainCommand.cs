using MedicineOrder.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.DomainCommands
{
    public class UpsertPharmacyDomainCommand
    {
        public int Id { get; set; }
        public string PharmacyName { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
