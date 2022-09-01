using Consumer.WorkerService;
using Infrastructure.Kafka;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>()
            .AddKafkaConsumer(hostContext.Configuration);
    })
    .Build();

await host.RunAsync();
