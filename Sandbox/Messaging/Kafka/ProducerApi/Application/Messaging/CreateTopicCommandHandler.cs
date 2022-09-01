using Domain.SharedKernel;
using MediatR;

namespace Application.Messaging;

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand>
{
    private readonly ITopicAdministrator _topicAdmin;

    public CreateTopicCommandHandler(ITopicAdministrator topicAdmin)
    {
        _topicAdmin = topicAdmin;
    }

    public async Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        await _topicAdmin.CreateTopicAsync(new TopicInfo(request.Topic, request.PartitionCount, request.ReplicationFactor), cancellationToken);

        return Unit.Value;
    }
}
