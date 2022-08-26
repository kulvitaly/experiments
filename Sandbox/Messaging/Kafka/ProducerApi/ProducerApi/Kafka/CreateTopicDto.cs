using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Kafka;

public class CreateTopicDto
{
    [FromRoute]
    public string Topic { get; set; }
}
