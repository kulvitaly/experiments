using RealEstateApi.Queries;

namespace RealEstateApi.Schema;

public class RealEstateSchema : GraphQL.Types.Schema
{
    public RealEstateSchema(IServiceProvider provider) : base(provider)
    {
        Query = provider.GetRequiredService<PropertyQuery>();
    }
}
