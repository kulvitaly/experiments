namespace Domain.SharedKernel;

public record TopicInfo(string Name, int PartitionCount, short ReplicationFactor);
