using Domain.SharedKernel;
using MediatR;

namespace Application.Messaging;

public class SendMessageCommand : IRequest
{
    public string Topic { get; }
    public Message Message { get; }

    public SendMessageCommand(string topic, Message message)
    {
        Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}
