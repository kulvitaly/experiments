using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema.Queries;

public class CourseType : ISearchResultType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }

    [IsProjected(true)]
    public Guid InstructorId { get; set; }

    [GraphQLNonNullType]
    public async Task<InstructorType> Instructor([Service] InstructorDataLoader loader)
    {
        var dto = await loader.LoadAsync(InstructorId);

        return new()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Salary = dto.Salary
        };
    }

    public IEnumerable<StudentType> Students { get; set; } = new List<StudentType>();

    [IsProjected(true)]
    public string? CreatorId { get; set; }


    public async Task<UserType?> Creator([Service] UserDataLoader userDataLoader)
    {
        if (CreatorId == null)
        {
            return null;
        }

        return await userDataLoader.LoadAsync(CreatorId);
    }

    public async Task<string> Description()
    {
        await Task.Delay(1000);
        return $"{Name} is a {Subject} course with {Students.Count()} students.";
    }
}
