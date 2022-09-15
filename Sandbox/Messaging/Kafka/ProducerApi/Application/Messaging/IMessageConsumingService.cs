namespace Application.Messaging;

public interface IMessageConsumingService
{
    event EventHandler<MessageReceivedEventArgs>? MessageReceived;

    Task RunAsync(CancellationToken cancellationToken);
}
