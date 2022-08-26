using MediatR;

namespace Application.Messaging;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IMessageSender _sendingService;

    public SendMessageCommandHandler(IMessageSender sendingService) => _sendingService = sendingService;

    public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        await _sendingService.SendAsync(request.Topic, request.Message, cancellationToken);

        return Unit.Value;
    }
}
