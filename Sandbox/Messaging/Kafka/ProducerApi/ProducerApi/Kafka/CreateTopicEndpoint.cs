using Application.Messaging;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ProducerApi.Kafka;

public class CreateTopicEndpoint : EndpointBaseAsync
  .WithRequest<CreateTopicDto>
  .WithActionResult<bool>
{
    private readonly IMediator _mediator;

    public CreateTopicEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/Kafka/Topic/{Topic}")]
    [SwaggerOperation(
        Summary = "Create message bus topic",
        Description = "Create message bus topic",
        OperationId = "Kafka.CreateTopic",
        Tags = new[] { "TopicEndpoint" })]
    public override async Task<ActionResult<bool>> HandleAsync(CreateTopicDto request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new CreateTopicCommand(request.Topic, request.PartitionCount, request.ReplicationFactor), cancellationToken);

        return Ok();
    }
}
