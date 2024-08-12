using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.DataLoaders;

public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDto>
{
    private readonly InstructorsRepository _instructorsRepository;

    public InstructorDataLoader(
        InstructorsRepository instructorsRepository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _instructorsRepository = instructorsRepository;
    }

    protected override async Task<IReadOnlyDictionary<Guid, InstructorDto>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var instructors = await _instructorsRepository.GetInstructorsByIds(keys, cancellationToken);

        return instructors.ToDictionary(i => i.Id);
    }
}
