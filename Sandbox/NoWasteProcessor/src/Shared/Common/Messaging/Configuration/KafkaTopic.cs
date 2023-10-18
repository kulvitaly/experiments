namespace Application.Messaging.Configuration;

public class KafkaTopic
{
    public string TopicName { get; init; } = string.Empty;

    public int PartitionCount { get; init; } = 1;

    public short ReplicationFactor { get; init; } = 1;
}
