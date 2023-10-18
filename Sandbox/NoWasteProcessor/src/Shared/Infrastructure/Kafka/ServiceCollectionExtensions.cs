using Common.Messaging.Administration;
using Common.Messaging.Consumption;
using Common.Messaging.Sending;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Kafka;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaProducer(this IServiceCollection services, IConfiguration configuration, string kafkaSection = KafkaOptions.Kafka)
    {
        return services
            .Configure<KafkaOptions>(configuration.GetSection(kafkaSection))
            .AddSingleton<KafkaMessageSendingService>()
            .AddSingleton<ITopicAdministrator, KafkaMessageSendingService>()
            .AddSingleton<IMessageSender, KafkaMessageSendingService>();
    }

    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, IConfiguration configuration, string kafkaSection = KafkaOptions.Kafka)
    {
        return services
            .Configure<KafkaConsumerOptions>(configuration.GetSection(kafkaSection))
            .AddSingleton<IMessageConsumingService, KafkaMessageConsumingService>();
    }
}
