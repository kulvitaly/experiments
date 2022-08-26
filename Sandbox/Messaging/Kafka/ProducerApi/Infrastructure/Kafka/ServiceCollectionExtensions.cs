using Application.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Kafka;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration, string kafkaSection = KafkaOptions.Kafka)
    {
        return services
            .Configure<KafkaOptions>(configuration.GetSection(kafkaSection))
            .AddSingleton<KafkaMessageSendingService>()
            .AddSingleton<ITopicAdministrator, KafkaMessageSendingService>()
            .AddSingleton<IMessageSender, KafkaMessageSendingService>();
    }
}
