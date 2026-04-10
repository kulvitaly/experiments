using BudgetManager.Api.GraphQL.Families;
using BudgetManager.Domain.Users;
using HotChocolate.Types;

namespace BudgetManager.Api.GraphQL.Users;

public class UserObjectType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<User>().Id.Value);

        descriptor.Field(u => u.Name)
            .Type<NonNullType<StringType>>();

        descriptor.Field(u => u.IconUrl)
            .Type<NonNullType<StringType>>();

        descriptor.Field(u => u.FamilyId)
            .Type<UuidType>()
            .Resolve(ctx => ctx.Parent<User>().FamilyId?.Value);

        descriptor.Field("family")
            .Type<FamilyObjectType>()
            .Resolve(ctx =>
            {
                var familyId = ctx.Parent<User>().FamilyId;
                if (familyId is null) return null;
                return ctx.DataLoader<FamilyByIdDataLoader>()
                    .LoadAsync(familyId.Value, ctx.RequestAborted);
            });

        descriptor.Ignore(u => u.WalletIds);
    }
}
