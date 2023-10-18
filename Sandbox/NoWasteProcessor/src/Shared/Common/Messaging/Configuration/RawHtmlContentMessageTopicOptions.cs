namespace Application.Messaging.Configuration;

public class RawHtmlContentMessageTopicOptions : KafkaTopic
{
    public const string RawHtmlContentMessage = "RawHtmlContentMessage";

    public RawHtmlContentMessageTopicOptions()
    {
        TopicName = RawHtmlContentMessage;
    }
}
