using Domain.SharedKernel;

namespace Common.Messaging.Sending;

public interface IMessageSender
{
    Task Send(string topic, Message message, CancellationToken cancellationToken);
}
