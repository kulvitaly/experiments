using Domain.SharedKernel;

namespace Common.Messaging.Administration;

public interface ITopicAdministrator
{
    Task CreateTopic(TopicInfo topicSpec, CancellationToken cancellationToken);
}
