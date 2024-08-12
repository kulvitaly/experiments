using DataAccess.Repositories.Contracts;
using Database;
using Database.Models;

namespace DataAccess.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly RealEstateContext _context;

    public PropertyRepository(RealEstateContext context)
    {
        _context = context;
    }

    public IEnumerable<Property> GetAll()
    {
        return _context.Properties;
    }
}
