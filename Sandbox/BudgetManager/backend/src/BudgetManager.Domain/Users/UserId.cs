using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Users;

public record UserId(Guid Value) : Key<Guid>(Value)
{
    public UserId() : this(Guid.CreateVersion7()) { }
}
