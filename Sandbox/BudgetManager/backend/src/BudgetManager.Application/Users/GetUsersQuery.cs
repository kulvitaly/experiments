using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Users;

public record GetUsersQuery : IQuery<IReadOnlyList<User>>;

internal sealed class GetUsersQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetUsersQuery, IReadOnlyList<User>>
{
    public async Task<IReadOnlyList<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await db.Users.ToListAsync(cancellationToken);
}
