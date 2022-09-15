namespace Infrastructure.Kafka;

public class KafkaConsumerOptions : KafkaOptions
{
    public string[] Topics { get; set; }

    public string GroupId { get; set; }
}
