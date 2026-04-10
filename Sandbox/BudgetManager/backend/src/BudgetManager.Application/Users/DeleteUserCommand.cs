using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Users;
using MediatR;

namespace BudgetManager.Application.Users;

public record DeleteUserCommand(Guid Id) : ICommand<bool>;

internal sealed class DeleteUserCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync([new UserId(request.Id)], cancellationToken);
        if (user is null) 
            return false;

        db.Users.Remove(user);
        return true;
    }
}
