using Azure.Messaging.ServiceBus.Administration;
using Azure.Messaging.ServiceBus;
using Medication.Application.Medication.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Medication.Application.Medication.Handlers
{
    public class BroadcastMedicationHandler : IBroadcastMedicationHandler
    {
        public async Task BroadcastMedicationAsync(BroadcastMedicationDto dtoMsg)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            string ServiceBusConnectionString = _configurationRoot.GetSection("AzureServiceBus")["PrimaryConnectionStrings"];
            string TopicName = _configurationRoot.GetSection("AzureServiceBus")["TopicName"];
            string SubscriptionOfMedicationOrder = _configurationRoot.GetSection("AzureServiceBus")["SubscriptionName1"];
            string SubscriptionOfPharmacy = _configurationRoot.GetSection("AzureServiceBus")["SubscriptionName2"];

            //create an administration client to manage artifacts
            var serviceBusAdministrationClient = new ServiceBusAdministrationClient(ServiceBusConnectionString);

            //create a new topic if it does not exist
            if (!await serviceBusAdministrationClient.TopicExistsAsync(TopicName))
            {
                await serviceBusAdministrationClient.CreateTopicAsync(TopicName);
            }

            //create a new supscription if it does not exist
            if (!await serviceBusAdministrationClient.SubscriptionExistsAsync(TopicName, SubscriptionOfMedicationOrder))
            {
                var options = new CreateSubscriptionOptions(TopicName, SubscriptionOfMedicationOrder)
                {
                    ForwardTo = SubscriptionOfMedicationOrder,
                    MaxDeliveryCount = 10,
                };
                await serviceBusAdministrationClient.CreateSubscriptionAsync(options);
            }
            if (!await serviceBusAdministrationClient.SubscriptionExistsAsync(TopicName, SubscriptionOfPharmacy))
            {
                var options = new CreateSubscriptionOptions(TopicName, SubscriptionOfPharmacy)
                {
                    ForwardTo = SubscriptionOfPharmacy,
                    MaxDeliveryCount = 10,
                };
                await serviceBusAdministrationClient.CreateSubscriptionAsync(options);
            }

            //create a service bus client
            var servicebusClient = new ServiceBusClient(ServiceBusConnectionString);
            //create a service bus sender
            var serviceBusSender = servicebusClient.CreateSender(TopicName);
            try
            {
                //create service bus message for subscription 1
                ServiceBusMessage message = new ServiceBusMessage(
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dtoMsg,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })));
                
                await serviceBusSender.SendMessageAsync(message);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await serviceBusSender.DisposeAsync();
                await servicebusClient.DisposeAsync();
            }
        }
    }
}
