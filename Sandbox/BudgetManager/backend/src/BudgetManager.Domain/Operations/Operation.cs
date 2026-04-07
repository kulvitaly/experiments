using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Operations;

public abstract class Operation : BaseEntity<OperationId>, IAggregateRoot
{
    public decimal Amount { get; protected set; }
    public DateTimeOffset OccurredOn { get; protected set; }

    protected Operation(decimal amount, DateTimeOffset occurredOn)
    {
        Id = new OperationId();
        Amount = amount;
        OccurredOn = occurredOn;
    }
}
