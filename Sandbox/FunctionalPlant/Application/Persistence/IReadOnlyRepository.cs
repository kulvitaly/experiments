using Model.Common;
using Models.Types;

namespace Application.Persistence;

public interface IReadOnlyRepository<T>
{
    IEnumerable<T> GetAll();

    Option<T> Find(Guid id);
}