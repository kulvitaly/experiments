using BudgetManager.Api.GraphQL.Users;
using BudgetManager.Domain.Wallets;
using HotChocolate.Types;

namespace BudgetManager.Api.GraphQL.Wallets;

public class WalletObjectType : ObjectType<Wallet>
{
    protected override void Configure(IObjectTypeDescriptor<Wallet> descriptor)
    {
        descriptor.Field(w => w.Id)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<Wallet>().Id.Value);

        descriptor.Field(w => w.Name)
            .Type<NonNullType<StringType>>();

        descriptor.Field(w => w.IconUrl)
            .Type<NonNullType<StringType>>();

        descriptor.Field(w => w.Type)
            .Type<NonNullType<EnumType<WalletType>>>();

        descriptor.Field(w => w.OwnerId)
            .Type<NonNullType<UuidType>>()
            .Resolve(ctx => ctx.Parent<Wallet>().OwnerId.Value);

        descriptor.Field("owner")
            .Type<NonNullType<UserObjectType>>()
            .Resolve(ctx =>
                ctx.DataLoader<UserByIdDataLoader>()
                    .LoadAsync(ctx.Parent<Wallet>().OwnerId.Value, ctx.RequestAborted));

        descriptor.Ignore(w => w.IsDeleted);
        descriptor.Ignore(w => w.DeletedAt);
    }
}
