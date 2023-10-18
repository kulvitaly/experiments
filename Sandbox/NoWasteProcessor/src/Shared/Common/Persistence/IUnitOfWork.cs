namespace Application.Common.Persistence;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}
