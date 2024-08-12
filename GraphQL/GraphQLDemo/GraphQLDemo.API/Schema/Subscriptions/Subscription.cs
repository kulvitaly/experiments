using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

    [Subscribe]
    public InstructorResult InstructorCreated([EventMessage] InstructorResult instructor) => instructor;

    [SubscribeAndResolve]
    public async ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver eventReceiver)
    {
        var topic = $"{courseId}_{nameof(CourseUpdated)}";
        return await eventReceiver.SubscribeAsync<CourseResult>(topic);
    }
}
