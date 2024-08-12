using DataAccess.Repositories.Contracts;
using GraphQL.Types;
using Types;

namespace RealEstateApi.Queries;

public class PropertyQuery : ObjectGraphType
{
    public PropertyQuery(IPropertyRepository repository)
    {
        Field<ListGraphType<PropertyType>>(
            "properties",
            resolve: context => repository.GetAll());
    }
}
