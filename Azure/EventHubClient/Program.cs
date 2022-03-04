using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;

namespace EventHubClient
{
    class Program
    {
        private const string ConnectionString = "Endpoint=sb://evhns-wired-brain.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cz7vcNdgeXp+0B4ZftOkSfXLsL1k7U2/D6MLpGsFZF8=";
        private const string EventHubName = "evh-wired-brain";

        private const string ConsumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

        static async Task Main(string[] args)
        {
            await GetPartitionInfo();
            await ReadFromPartition("0");
            await ReadFromPartition("1");
            await ReadFromPartition("2");
            
        }

        private static async Task ReadFromPartition(string partitionNumber)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(120));

            await using var consumerClient = new EventHubConsumerClient(ConsumerGroup, ConnectionString, EventHubName);

            try
            {
                var props = await consumerClient.GetPartitionPropertiesAsync(partitionNumber);

                var startingPosition = EventPosition.FromSequenceNumber(
                    //props.LastEnqueuedSequenceNumber
                    props.BeginningSequenceNumber);

                await foreach (PartitionEvent partitionEvent in consumerClient.ReadEventsFromPartitionAsync(partitionNumber, startingPosition, cancellationTokenSource.Token))
                {
                    Console.WriteLine("***** NEW COFFEE *****");

                    var partitionId = partitionEvent.Partition.PartitionId;
                    var sequenceNumber = partitionEvent.Data.SequenceNumber;
                    var key = partitionEvent.Data.PartitionKey;

                    Console.WriteLine($"Partition Id: {partitionId}{Environment.NewLine}"
                        + $"SenquenceNumber: {sequenceNumber}{Environment.NewLine}"
                        + $"Partition key: {key}");

                    var coffee = JsonSerializer.Deserialize<CoffeeData>(partitionEvent.Data.EventBody.ToArray());
                    
                    Console.WriteLine($"Temperature: {coffee.WaterTemperature}, time: {coffee.BeadingTime}, type: {coffee.CoffeeType}");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally 
            {
                await consumerClient.CloseAsync();
            }
        }
        private static async Task GetPartitionInfo()
        {
            await using var consumerClient = new EventHubConsumerClient(ConsumerGroup, ConnectionString, EventHubName);

            var partitionIds = await consumerClient.GetPartitionIdsAsync();

            foreach (var id in partitionIds)
            {
                var partitionInfo = await consumerClient.GetPartitionPropertiesAsync(id);

                Console.WriteLine("***** NEW PARTITION INFO ******");
                Console.WriteLine($"Partition id: {partitionInfo.Id}{Environment.NewLine}"
                 + $"Empty? {partitionInfo.IsEmpty}{Environment.NewLine}"
                 + $"Last Sequence: {partitionInfo.LastEnqueuedSequenceNumber}{Environment.NewLine}"
                 + $"First Sequence: {partitionInfo.BeginningSequenceNumber}{Environment.NewLine}");
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
}
