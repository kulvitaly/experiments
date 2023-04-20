using Application.Messaging;

namespace Consumer.WorkerService;

public class Worker : BackgroundService
{
    private readonly IMessageConsumingService _messageConsumingService;
    private readonly ILogger<Worker> _logger;

    public Worker(IMessageConsumingService messageConsumingService, ILogger<Worker> logger)
    {
        _messageConsumingService = messageConsumingService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start consuming messages");

        _messageConsumingService.MessageReceived += HandleMessageReceived;

        await _messageConsumingService.RunAsync(stoppingToken);

        _logger.LogInformation("Stop consuming messages");
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
        _logger.LogInformation("Message received: Key: {Key} Message: {Message}, ", eventArgs.Message.Key, eventArgs.Message.Text);
    }
}