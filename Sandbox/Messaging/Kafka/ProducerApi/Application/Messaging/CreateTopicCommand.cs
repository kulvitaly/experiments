using Ardalis.GuardClauses;
using MediatR;

namespace Application.Messaging;

public class CreateTopicCommand : IRequest
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
