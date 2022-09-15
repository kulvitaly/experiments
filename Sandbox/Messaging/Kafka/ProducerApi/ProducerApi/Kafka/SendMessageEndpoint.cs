using Application.Messaging;
using Ardalis.ApiEndpoints;
using Domain.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProducerApi.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace ProducerApi.Kafka;

public class SendMessageEndpoint : EndpointBaseAsync
      .WithRequest<SendMessageDto>
      .WithActionResult<bool>
{
    private readonly IMediator _mediator;

    public SendMessageEndpoint(IMediator mediator) => _mediator = mediator;

    [HttpPost("api/Kafka/{Topic}/message")]
    [SwaggerOperation(
    Summary = "Sends message via messaging Bus",
    Description = "Sends message via messaging Bus",
    OperationId = "Producer.Send",
    Tags = new[] { "ProducerEndpoint" })]
    public override async Task<ActionResult<bool>> HandleAsync([FromMultiSource] SendMessageDto request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SendMessageCommand(request.Topic, new Message(request.Info.Key, request.Info.Message, DateTimeOffset.UtcNow)), cancellationToken);

        return Ok(true);
    }
}