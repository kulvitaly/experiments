using Application.Common.MediatR;
using Application.Messaging.Configuration;
using Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Messaging.Sending;

public record SendRawHtmlContentMessageCommand(string HtmlContent) : ICommand;

public class SendRawHtmlContentMessageCommandHandler : IRequestHandler<SendRawHtmlContentMessageCommand>
{
    private readonly IMediator _mediator;
    private readonly RawHtmlContentMessageTopicOptions _options;

    public SendRawHtmlContentMessageCommandHandler(IMediator mediator, IOptions<RawHtmlContentMessageTopicOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<Unit> Handle(SendRawHtmlContentMessageCommand request, CancellationToken cancellationToken)
        => await _mediator.Send(
            new SendMessageCommand(_options.TopicName, new RawHtmlContentMessage(request.HtmlContent, DateTimeOffset.UtcNow)),
            cancellationToken);
}