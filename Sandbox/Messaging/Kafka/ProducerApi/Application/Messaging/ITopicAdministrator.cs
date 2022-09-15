using Domain.SharedKernel;

namespace Application.Messaging;

public interface ITopicAdministrator
{
    Task CreateTopicAsync(TopicInfo topicSpec, CancellationToken cancellationToken);
}
