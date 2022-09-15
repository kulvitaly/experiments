using Application.Messaging;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Kafka;

internal class KafkaMessageConsumingService : IMessageConsumingService
{
    private readonly KafkaConsumerOptions _options;
    private readonly ILogger<KafkaMessageConsumingService> _logger;

    public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

    public KafkaMessageConsumingService(IOptions<KafkaConsumerOptions> options, ILogger<KafkaMessageConsumingService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task RunAsync(CancellationToken cancellationToken)
    {
        var config = CreateConsumerConfig(_options);

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(_options.Topics);

        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(cancellationToken);

            _logger.LogDebug("Received message: {RawMessage}", JsonSerializer.Serialize(consumeResult));

            var message = JsonSerializer.Deserialize<Domain.SharedKernel.Message>(consumeResult.Message.Value);

            if (message == null)
            {
                _logger.LogWarning("Deserialized message as null. Ignore message. Raw: {RawMessage}", JsonSerializer.Serialize(consumeResult));
                continue;
            }

            OnMessageReceived(message);
        }

        consumer.Close();

        return Task.CompletedTask;
    }

    private void OnMessageReceived(Domain.SharedKernel.Message message) => MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));

    private static ConsumerConfig CreateConsumerConfig(KafkaConsumerOptions options)
    {
        return new ConsumerConfig
        {
            BootstrapServers = options.BootstrapServer,
            GroupId = options.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}
