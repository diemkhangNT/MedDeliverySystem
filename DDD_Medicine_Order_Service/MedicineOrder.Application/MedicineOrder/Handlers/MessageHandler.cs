using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using MedicineOrder.Application.Medicine.Handlers;
using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Application.Pharmacy.Dtos;
using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IGetListMedicineOrderHandler _getListMedicineOrderHandler;
        private readonly IGetListMedicinesHandler _getListMedicines;

        public MessageHandler(IGetListMedicineOrderHandler getListMedicineOrderHandler, IGetListMedicinesHandler getListMedicines)
        {
            _getListMedicineOrderHandler = getListMedicineOrderHandler;
            _getListMedicines = getListMedicines;
        }

        public async Task BroadcastMedicineOrder(BroadcastOrderDto dto)
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

            var orderMessage = _getListMedicineOrderHandler.GetSingleMedicineOrder(new() { OrderId = dto.OrderId }).Result.FirstOrDefault();
            for (int i = 0; i < orderMessage.MedicineOrderDetails.Count; i++)
            {
                var medicine = _getListMedicines.GetSingleMedicineAsync(new() { MedicineId = orderMessage.MedicineOrderDetails[i].MedicineID }).Result.FirstOrDefault();
                orderMessage.MedicineOrderDetails[i].MedicineDetail = medicine;
            }

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await serviceBusSender.CreateMessageBatchAsync();

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(orderMessage,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })))))
            {
                // if it is too large for the batch
                throw new Exception($"The message {orderMessage} is too large to fit in the batch.");
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
