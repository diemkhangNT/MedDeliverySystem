using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MedicineOrder.Application.Pharmacy.Handlers;
using MedicineOrder.Application.Pharmacy.Dtos;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using System.Text;
using MedicineOrder.Application.Medicine.Dtos;

namespace PharmacyHandler
{
    public class PharmacyHandler
    {
        private readonly IMessageHandler _messageHandler;

        public PharmacyHandler(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [FunctionName("Pharmacy_Receive")]
        public async Task Run([ServiceBusTrigger("medication-order-receive", "all", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message, ILogger log)
        {
            try
            {
                var getSourceDto = JsonConvert.DeserializeObject<GetSourceDto>(Encoding.UTF8.GetString(message.Body));
                if (getSourceDto.Source.Equals("Pharmacy"))
                {
                    var upsertPharmacyDto = JsonConvert.DeserializeObject<UpsertPharmacyDto>(Encoding.UTF8.GetString(message.Body));
                    await _messageHandler.UpdatePharmacyAsync(upsertPharmacyDto);
                    log.LogInformation($"{upsertPharmacyDto}");
                }
                if (getSourceDto.Source.Equals("Medication"))
                {
                    var upsertMedicationDto = JsonConvert.DeserializeObject<UpsertMedicationDto>(Encoding.UTF8.GetString(message.Body));
                    
                    await _messageHandler.UpdateMedicationAsync(upsertMedicationDto);
                    log.LogInformation($"{upsertMedicationDto}");
                }
            }
            catch (Exception ex)
            {
                log.LogError($"{ex}");
            }
        }

    }
}
