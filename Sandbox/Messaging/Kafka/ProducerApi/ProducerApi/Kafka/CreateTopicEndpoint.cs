using Application.Messaging;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Kafka
{
    public class CreateTopicEndpoint : EndpointBaseAsync
      .WithRequest<CreateTopicDto>
      .WithActionResult<bool>
    {
        private readonly IMediator _mediator;

        public CreateTopicEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<ActionResult<bool>> HandleAsync(CreateTopicDto request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new CreateTopicCommand(request.Topic), cancellationToken);

            return Ok();
        }
    }
}
