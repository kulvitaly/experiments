using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Operations;

public record OperationId(Guid Value) : Key<Guid>(Value)
{
    public OperationId() : this(Guid.CreateVersion7()) { }
}
