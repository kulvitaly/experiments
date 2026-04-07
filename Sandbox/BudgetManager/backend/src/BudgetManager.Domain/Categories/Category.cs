using BudgetManager.Domain.Common;
using BudgetManager.Domain.Families;

namespace BudgetManager.Domain.Categories;

public class Category : BaseEntity<CategoryId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public CategoryType Type { get; private set; }
    public FamilyId FamilyId { get; private set; }

    public Category(string name, string iconUrl, CategoryType type, FamilyId familyId)
    {
        Id = new CategoryId();
        Name = name;
        IconUrl = iconUrl;
        Type = type;
        FamilyId = familyId;
    }
}
