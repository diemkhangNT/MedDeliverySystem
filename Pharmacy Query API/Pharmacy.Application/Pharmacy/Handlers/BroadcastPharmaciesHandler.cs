using Azure.Messaging.ServiceBus.Administration;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Pharmacy.Dtos;
using Pharmacy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pharmacy.Application.Pharmacy.Handlers
{
    public class BroadcastPharmaciesHandler : IBroadcastPharmaciesHandler
    {

        private readonly IPharmacyRepository _pharmacyRepository;

        public BroadcastPharmaciesHandler(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public async Task BroadcastPharmacies()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            string ServiceBusConnectionString = _configurationRoot.GetSection("AzureServiceBus")["PrimaryConnectionStrings"];
            string TopicName = _configurationRoot.GetSection("AzureServiceBus")["TopicName"];
            string SubscriptionName = _configurationRoot.GetSection("AzureServiceBus")["SubscriptionName"];

            //create an administration client to manage artifacts
            var serviceBusAdministrationClient = new ServiceBusAdministrationClient(ServiceBusConnectionString);

            //create a new topic if it does not exist
            if (!await serviceBusAdministrationClient.TopicExistsAsync(TopicName))
            {
                await serviceBusAdministrationClient.CreateTopicAsync(TopicName);
            }

            //create a new supscription if it does not exist
            if (!await serviceBusAdministrationClient.SubscriptionExistsAsync(TopicName, SubscriptionName))
            {
                var options = new CreateSubscriptionOptions(TopicName, SubscriptionName)
                {
                    ForwardTo = SubscriptionName,
                    MaxDeliveryCount = 10,
                };
                await serviceBusAdministrationClient.CreateSubscriptionAsync(options);
            }

            //create a service bus client
            var servicebusClient = new ServiceBusClient(ServiceBusConnectionString);
            //create a service bus sender
            var serviceBusSender = servicebusClient.CreateSender(TopicName);

            var listPharmacies = await _pharmacyRepository.GetAllOfPharmacies(new(pharmacy => !pharmacy.IsDeleted));
            var response = listPharmacies.Select(pharmacy => new PharmacyDto(pharmacy)).ToList();

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await serviceBusSender.CreateMessageBatchAsync();

            foreach(var message in response)
            {
                message.Source = "Pharmacy";
                // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, 
                    new JsonSerializerOptions(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase })))))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message {message} is too large to fit in the batch.");
                }
            }

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus topic
                await serviceBusSender.SendMessagesAsync(messageBatch);
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
