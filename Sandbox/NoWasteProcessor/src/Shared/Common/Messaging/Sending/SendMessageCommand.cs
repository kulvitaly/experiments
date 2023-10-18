using Application.Common.MediatR;
using Common.Messaging.Sending;
using Domain.SharedKernel;
using MediatR;

namespace Application.Messaging.Sending;

public class SendMessageCommand : ICommand
{
    public string Topic { get; }
    public Message Message { get; }

    public SendMessageCommand(string topic, Message message)
    {
        Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IMessageSender _sendingService;

    public SendMessageCommandHandler(IMessageSender sendingService) => _sendingService = sendingService;

    public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        await _sendingService.Send(request.Topic, request.Message, cancellationToken);

        return Unit.Value;
    }
}


