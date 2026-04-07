using BudgetManager.Domain.Families;
using HotChocolate.Types;

namespace BudgetManager.Api.GraphQL.Families;

public class FamilyObjectType : ObjectType<Family>
{
    protected override void Configure(IObjectTypeDescriptor<Family> descriptor)
    {
        descriptor.Field(f => f.Id)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<Family>().Id.Value);

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.IconUrl)
            .Type<StringType>();

        descriptor.Ignore(f => f.UserIds);
    }
}
