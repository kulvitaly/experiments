using Database.Models;

namespace DataAccess.Repositories.Contracts;

public interface IPropertyRepository
{
    IEnumerable<Property> GetAll();
}
