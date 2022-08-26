using MediatR;

namespace Application.Messaging;

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand>
{
    public Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
