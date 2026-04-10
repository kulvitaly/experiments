using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
using MediatR;

namespace BudgetManager.Application.Users;

public record CreateUserCommand(string Name, string IconUrl, Guid? FamilyId) : ICommand<User>;

internal sealed class CreateUserCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<CreateUserCommand, User>
{
    public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.FamilyId.HasValue
            ? new User(request.Name, request.IconUrl, new FamilyId(request.FamilyId.Value))
            : new User(request.Name, request.IconUrl);

        db.Users.Add(user);
        return Task.FromResult(user);
    }
}
