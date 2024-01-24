using MedicineOrder.Domain.DomainCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Entities
{
    public class Medicine : EntityBase
    {
        public Medicine()
        {
        }

        public Medicine(CreateMedicineDomainCommand command)
        {
            Id = command.MedicineId;
            Name = command.MedicineName;
            Price = command.Price;
            ModifiedBy = CreatedBy = "Khang Nguyen";
            ModifiedUtc = CreatedUtc = DateTime.UtcNow.ToLocalTime();
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<MedicineOrder> MedicineOrders { get; private set; }

        public void Update(CreateMedicineDomainCommand command)
        {
            Name = command.MedicineName;
            Price = command.Price;
            ModifiedBy = "Khang Nguyen";
            ModifiedUtc = DateTime.UtcNow.ToLocalTime();
        }
    }
}
