using Domain.SharedKernel;

namespace Application.Messaging;

public interface IMessageSender
{
    Task SendAsync(string topic, Message message, CancellationToken cancellationToken);
}
