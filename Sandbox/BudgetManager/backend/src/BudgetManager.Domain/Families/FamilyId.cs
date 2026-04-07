using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Families;

public record FamilyId(Guid Value) : Key<Guid>(Value)
{
    public FamilyId() : this(Guid.CreateVersion7()) { }
}
