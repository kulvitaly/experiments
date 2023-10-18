using Application.Messaging.Consumption;

namespace Common.Messaging.Consumption;

public interface IMessageConsumingService
{
    event EventHandler<MessageReceivedEventArgs>? MessageReceived;

    Task RunAsync(CancellationToken cancellationToken);
}
