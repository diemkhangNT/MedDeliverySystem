using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Application.Pharmacy.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public interface IMessageHandler
    {
        Task BroadcastMedicineOrder(BroadcastOrderDto dto);
    }
}
