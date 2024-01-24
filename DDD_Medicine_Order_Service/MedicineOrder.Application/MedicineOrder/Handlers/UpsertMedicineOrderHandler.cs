using MedicineOrder.Application.Exceptions;
using MedicineOrder.Application.Medicine.Handlers;
using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Exceptions;
using MedicineOrder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public class UpsertMedicineOrderHandler : IUpsertMedicineOrderHandler
    {
        private readonly IMedicineOrderRepository _medicineOrderRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IGetListMedicinesHandler _listMedicinesHandler;
        private readonly IMessageHandler _broadcastMedicineOrderHandler;

        public UpsertMedicineOrderHandler(IMedicineOrderRepository medicineOrderRepository, IMedicineRepository medicineRepository, IMessageHandler broadcastMedicineOrderHandler)
        {
            _medicineOrderRepository = medicineOrderRepository;
            _medicineRepository = medicineRepository;
            _listMedicinesHandler = new GetListMedicinesHandler(_medicineRepository);
            _broadcastMedicineOrderHandler = broadcastMedicineOrderHandler;
        }

        public async Task InsertOrderHandler(UpsertOrderDto dto)
        {
            var medicineOrder = await _medicineOrderRepository.GetSingleMedicineOrder(new(medOder => medOder.Id == dto.Id));
            
            if(medicineOrder == null)
            {
                var insertOrderDomaindCommand = dto.ConvertToOrderDomainCommand()
                    ?? throw new ArgumentNullException();

                //Get list medicine 
                var listMedicinesDto = await _listMedicinesHandler.GetListMedicinesAsync()
                    ?? throw new ArgumentNullException();
                var listCreateMedicineCommand = new List<CreateMedicineDomainCommand>();
                foreach(var item in listMedicinesDto)
                {
                    listCreateMedicineCommand.Add(item.ConvertMedicineDtoToCreateMedicineDomainCommand());
                }

                medicineOrder = new Domain.Entities.Order(insertOrderDomaindCommand, listCreateMedicineCommand);
                await _medicineOrderRepository.InsertMedicineOrder(medicineOrder);
                try
                {
                    await _medicineOrderRepository.UnitOfWork.SaveEntityChangesAsync();
                    //change dto's TotalPrice value to test 
                    dto.TotalPrice = medicineOrder.TotalPrice;
                    await _broadcastMedicineOrderHandler.BroadcastMedicineOrder(new() { OrderId = medicineOrder.Id });
                }
                catch (DbUpdateException ex)
                {
                    throw new InvalidArgumentException(nameof(medicineOrder.PharmacyID), "is invalid");
                }
            }
            else
            {
                throw new MedicineOrderAlreadyExistedException(medicineOrder.Id);
            }
        }

        public async Task DeleteMedicineOrderHandler(UpsertOrderDto dto)
        {
            var medicine = await _medicineOrderRepository.GetSingleMedicineOrder(new(order => order.Id == dto.Id))
                ?? throw new MedicineOrderNotFoundException(dto.Id);
            medicine.IsDeleted = true;
            _medicineOrderRepository.UpdateMedicineOrder(medicine);
            await _medicineOrderRepository.UnitOfWork.SaveEntityChangesAsync();
        }
    }
}
