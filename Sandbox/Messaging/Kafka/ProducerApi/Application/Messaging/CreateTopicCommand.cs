using Ardalis.GuardClauses;
using MediatR;

namespace Application.Messaging;

public class CreateTopicCommand : IRequest
{
    public CreateTopicCommand(string topic)
    {
        Guard.Against.NullOrWhiteSpace(topic, nameof(topic), "Topic can not be null or empty");

        Topic = topic;
    }

    public string Topic { get; }
}
