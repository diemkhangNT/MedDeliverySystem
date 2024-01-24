using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Domain.DomainCommands
{
    public record UpsertMedicationDomandCommand
    {
        public string MedicineName { get; set; }
        public decimal Price { get; set; }
    }
}
