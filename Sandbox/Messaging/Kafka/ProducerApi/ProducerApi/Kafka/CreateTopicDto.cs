using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Kafka;

public class CreateTopicDto
{
    [FromRoute]
    public string Topic { get; set; }

    [FromBody]
    public int PartitionCount { get; set; } = 1;

    [FromBody]
    public short ReplicationFactor { get; set; } = 1;
}
