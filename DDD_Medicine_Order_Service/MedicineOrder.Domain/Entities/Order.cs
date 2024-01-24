using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Exceptions;
using MedicineOrder.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Entities
{
    public class Order : EntityBase
    {
        public Order()
        {
        }

        public Order(InsertOrderDomainCommand command, ICollection<CreateMedicineDomainCommand> medicinesCommand)
        {
            ValidateMedicineOrderDetail(command.MedicineOrderDetails);
            ValidateListMedicines(medicinesCommand);
            //Caculate Total Price and set MedicineOrders
            CalculateTotalPrice(command.MedicineOrderDetails, medicinesCommand);
            
            OrderStatus = command.OrderStatus;
            PharmacyID = command.PharmacyID;
            ModifiedBy = CreatedBy = "Khang NTD";
            ModifiedUtc = CreatedUtc = DateTime.UtcNow.ToLocalTime();
        }

        public decimal TotalPrice { get; private set; }
        public bool OrderStatus { get; private set; }
        public int PharmacyID { get; private set; }
        public Pharmacy Pharmacy { get; private set; }
        public ICollection<MedicineOrder> MedicineOrders { get; private set; }

        private void ValidateMedicineOrderDetail(ICollection<InsertMedicineOrderDomainCommand> MedicineOrders)
        {
            if (MedicineOrders.Count == 0)
                throw new MedicineOrdersIsNullException();
        }

        private void ValidateListMedicines(ICollection<CreateMedicineDomainCommand> Medicines)
        {
            if(Medicines.Count == 0)
                throw new MedicineIsNotExistedException();
        }

        private void CalculateTotalPrice(ICollection<InsertMedicineOrderDomainCommand> medicineOrders, ICollection<CreateMedicineDomainCommand> medicinesCommand)
        {
            // Calculator total price
            decimal totalPrice = 0;
            List<InsertMedicineOrderDomainCommand> orderDetails = medicineOrders.OrderBy(x => x.MedicineID).ToList();
            var length = orderDetails.Count;
            for (int i = 0; i < length; i++)
            {
                var subList = orderDetails.Where(d => d.MedicineID == orderDetails[i].MedicineID).ToList().Skip(1);

                if (length > 1 && subList.Count() >= 1)
                {
                    int totalQuantity = 0; 
                    foreach (var item2 in subList)
                    {
                        totalQuantity += item2.Quantity;
                        orderDetails.RemoveAt(i + 1);
                        length--;
                    }
                    orderDetails[i].Quantity += totalQuantity;
                }
                var med = medicinesCommand.FirstOrDefault(m => m.MedicineId == orderDetails[i].MedicineID)
                    ?? throw new MedicineNotFoundException(orderDetails[i].MedicineID);
                totalPrice += orderDetails[i].Quantity * med.Price;
            }
            MedicineOrders = orderDetails.Select(medOrder => new MedicineOrder(medOrder)).ToList();
            TotalPrice = totalPrice;
        }
    }
}
