using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace eventhub
{
    class Program
    {
        static Random Rand = new();

        private const string ConnectionString = "Endpoint=sb://evhns-wired-brain.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cz7vcNdgeXp+0B4ZftOkSfXLsL1k7U2/D6MLpGsFZF8=";
        private const string EventHubName = "evh-wired-brain";
        
        static async Task Main(string[] args)
        {
            //await SendToRandomPartition();
            await SendToTheSamePartition();
        }

        private static async Task SendToRandomPartition()
        {
            await using var producerClient = new EventHubProducerClient(ConnectionString, EventHubName);

            using var eventBatch = await producerClient.CreateBatchAsync();

            for (int i = 0; i < 100; ++i)
            {
                var waterTemp = (Rand.NextDouble()) * 100;
                int coffeeTypeIndex = Rand.Next(2);

                var coffeeMachineData = new CoffeeData
                {
                    WaterTemperature = waterTemp,
                    BeadingTime = DateTime.Now,
                    CoffeeType = CoffeeData.AllCoffeeTypes[coffeeTypeIndex]
                };

                var coffeeMachineDataBytes = JsonSerializer.SerializeToUtf8Bytes(coffeeMachineData);

                var eventData = new EventData(coffeeMachineDataBytes);

                if (!eventBatch.TryAdd(eventData))
                {
                    throw new Exception("Can not add coffee machine data to random batch");
                }

            }
            
            await producerClient.SendAsync(eventBatch);

            Console.WriteLine("Wrote events to random partition");
        }

        private static async Task SendToTheSamePartition()
        {
            await using var producerClient = new EventHubProducerClient(ConnectionString, EventHubName);

            var eventBatch = new List<EventData>();

            for (int i = 0; i < 100; ++i)
            {
                var waterTemp = (Rand.NextDouble()) * 100;
                int coffeeTypeIndex = Rand.Next(2);

                var coffeeMachineData = new CoffeeData
                {
                    WaterTemperature = waterTemp,
                    BeadingTime = DateTime.Now,
                    CoffeeType = CoffeeData.AllCoffeeTypes[coffeeTypeIndex]
                };

                var coffeeMachineDataBytes = JsonSerializer.SerializeToUtf8Bytes(coffeeMachineData);
  
                var eventData = new EventData(coffeeMachineDataBytes);

                eventBatch.Add(eventData);
            }

            var options = new SendEventOptions();
            options.PartitionKey = "device1";

            await producerClient.SendAsync(eventBatch, options);

            Console.WriteLine("Wrote events to single partition");
        }

    }

    internal class CoffeeData
    {
        public double WaterTemperature { get; set; }
        public DateTime BeadingTime { get; set; }
        public object CoffeeType { get; set; }

        public static int[] AllCoffeeTypes = new[] {0, 1, 2};
    }
}
