using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pharmacy.Application.Medication.Dtos;
using Pharmacy.Application.Medication.Handlers;

namespace MedicationHandler
{
    public class MedicationHandler
    {
        private readonly IMessageHandler _messageHandler;

        public MedicationHandler(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [FunctionName("Medication_Receive_Function")]
        public async Task Run([ServiceBusTrigger("pharmacy-receive", "all", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message, ILogger log)
        {
            try
            {
                var medicationDto = JsonConvert.DeserializeObject<UpsertMedicationDto>(Encoding.UTF8.GetString(message.Body));
                if (medicationDto.Source.Equals("Medication"))
                {
                    await _messageHandler.UpsertMedicationAsync(medicationDto);
                    log.LogInformation($"{medicationDto}");
                }
                
            }
            catch (Exception ex)
            {
                log.LogError($"{ex}");
            }
        }
    }
}
