using BudgetManager.Domain.Categories;
using HotChocolate.Types;

namespace BudgetManager.Api.GraphQL.Categories;

public class CategoryObjectType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.Field(c => c.Id)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<Category>().Id.Value);

        descriptor.Field(c => c.Name)
            .Type<NonNullType<StringType>>();

        descriptor.Field(c => c.IconUrl)
            .Type<NonNullType<StringType>>();

        descriptor.Field(c => c.Type)
            .Type<NonNullType<EnumType<CategoryType>>>();

        descriptor.Field(c => c.FamilyId)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<Category>().FamilyId.Value);
    }
}
