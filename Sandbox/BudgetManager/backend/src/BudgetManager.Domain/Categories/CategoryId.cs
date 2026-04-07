using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Categories;

public record CategoryId(Guid Value) : Key<Guid>(Value)
{
    public CategoryId() : this(Guid.CreateVersion7()) { }
}
