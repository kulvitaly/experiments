using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Families;
using MediatR;

namespace BudgetManager.Application.Families;

public record RegisterFamilyCommand(string Name, string? IconUrl) : ICommand<Family>;

internal sealed class RegisterFamilyCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<RegisterFamilyCommand, Family>
{
    public Task<Family> Handle(RegisterFamilyCommand request, CancellationToken cancellationToken)
    {
        var family = new Family(request.Name, request.IconUrl ?? string.Empty);
        db.Families.Add(family);
        return Task.FromResult(family);
    }
}
