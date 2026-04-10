using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Users;
using MediatR;

namespace BudgetManager.Application.Users;

public record UpdateUserCommand(Guid Id, string Name, string IconUrl) : ICommand<User>;

internal sealed class UpdateUserCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync([new UserId(request.Id)], cancellationToken)
            ?? throw new InvalidOperationException($"User {request.Id} not found.");

        user.Update(request.Name, request.IconUrl);
        return user;
    }
}
