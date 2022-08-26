namespace Domain.SharedKernel;

public record Message(string? Key, string Text, DateTimeOffset DateTimeUtc);

public record TopicInfo(string Name, int PartitionCount, short ReplicationFactor);
