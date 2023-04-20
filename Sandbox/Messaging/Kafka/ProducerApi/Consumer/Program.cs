using Consumer.WorkerService;
using Infrastructure.Kafka;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    //.ConfigureLogging(loggingBuilder =>
    //{

    //    Log.Logger = new LoggerConfiguration()
    //              .ReadFrom.Configuration(loggingBuilder.Configuration)
    //              .CreateLogger();
    //})
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>()
            .AddKafkaConsumer(hostContext.Configuration)
            ;
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Consumer service started.");

await host.RunAsync();
