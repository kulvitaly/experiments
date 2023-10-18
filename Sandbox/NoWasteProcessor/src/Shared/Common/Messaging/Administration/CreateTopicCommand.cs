using Application.Common.MediatR;
using Ardalis.GuardClauses;
using Domain.SharedKernel;
using MediatR;

namespace Common.Messaging.Administration;

public class CreateTopicCommand : ICommand
{
    public CreateTopicCommand(string topic, int partitionCount, short replicationFactor)
    {
        Guard.Against.NullOrWhiteSpace(topic, nameof(topic), "Topic can not be null or empty");
        Guard.Against.NegativeOrZero(partitionCount, nameof(partitionCount), "Partition count can not be negative or zero");
        Guard.Against.NegativeOrZero(replicationFactor, nameof(replicationFactor), "Replication factor can not be negative or zero");

        Topic = topic;
        PartitionCount = partitionCount;
        ReplicationFactor = replicationFactor;
    }

    public string Topic { get; }

    public int PartitionCount { get; }

    public short ReplicationFactor { get; }
}

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand>
{
    private readonly ITopicAdministrator _topicAdmin;

    public CreateTopicCommandHandler(ITopicAdministrator topicAdmin)
    {
        _topicAdmin = topicAdmin;
    }

    public async Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        await _topicAdmin.CreateTopic(new TopicInfo(request.Topic, request.PartitionCount, request.ReplicationFactor), cancellationToken);

        return Unit.Value;
    }
}
