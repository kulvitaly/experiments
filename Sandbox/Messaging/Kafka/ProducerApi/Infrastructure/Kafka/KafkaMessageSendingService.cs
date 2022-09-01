using Application.Messaging;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Domain.SharedKernel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using System.Text.Json;

namespace Infrastructure.Kafka;

public class KafkaMessageSendingService : IMessageSender, ITopicAdministrator
{
    private readonly ILogger<KafkaMessageSendingService> _logger;
    private readonly AsyncLazy<ClientConfig> _clientConfigLazy;

    public KafkaMessageSendingService(IOptions<KafkaOptions> kafkaOptions, ILogger<KafkaMessageSendingService> logger)
    {
        _logger = logger;

        _clientConfigLazy = new(() => LoadConfigAsync(kafkaOptions.Value));
    }

    public async Task CreateTopicAsync(TopicInfo topicSpec, CancellationToken cancellationToken)
    {
        var config = await _clientConfigLazy;
        using (var adminClient = new AdminClientBuilder(config).Build())
        {
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                    new TopicSpecification { Name = topicSpec.Name, NumPartitions = topicSpec.PartitionCount, ReplicationFactor = topicSpec.ReplicationFactor } });
            }
            catch (CreateTopicsException e)
            {
                if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                {
                    _logger.LogError(e, "An error occured creating topic {Name}: {Reason}", topicSpec.Name, e.Results[0].Error.Reason);
                }
                else
                {
                    _logger.LogInformation(e, "Topic {Name} already exists", topicSpec.Name);
                }
            }
        }
    }

    public async Task SendAsync(string topic, Message message, CancellationToken cancellationToken)
    {
        var config = await _clientConfigLazy;
        using (var producer = new ProducerBuilder<string, string>(config).Build())
        {
            var key = message.Key;
            var value = JsonSerializer.Serialize(message);

            _logger.LogInformation("Producing record: {Key} {Value}", key, value);

            // TODO: use ProduceAsync
            producer.Produce(topic, new Message<string, string> { Key = key, Value = value },
                (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        _logger.LogError("Failed to deliver message: {Reason}", deliveryReport.Error.Reason);
                    }
                    else
                    {
                        _logger.LogInformation("Produced message to: {TopicPartitionOffset}", deliveryReport.TopicPartitionOffset);
                    }
                });

            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }

    // copied from example: https://github.com/confluentinc/examples/blob/7.2.1-post/clients/cloud/csharp/Program.cs
    private async Task<ClientConfig> LoadConfigAsync(KafkaOptions kafkaOptions)
    {
        return new ClientConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServer
        };
        //try
        //{
        //    var cloudConfig = (await File.ReadAllLinesAsync(configPath))
        //        .Where(line => !line.StartsWith("#") && !line.Length.Equals(0))
        //        .ToDictionary(
        //            line => line.Substring(0, line.IndexOf('=')),
        //            line => line.Substring(line.IndexOf('=') + 1));

        //    var clientConfig = new ClientConfig(cloudConfig);

        //    if (certDir != null)
        //    {
        //        clientConfig.SslCaLocation = certDir;
        //    }

        //    return clientConfig;
        //}
        //catch (Exception e)
        //{
        //    _logger.LogCritical(e, "An error occured reading the config file from '{ConfigPath}': {Message}", configPath);
        //    throw;
        //}
    }
}