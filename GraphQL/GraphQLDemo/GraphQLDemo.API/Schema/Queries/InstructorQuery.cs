using GraphQLDemo.API.Services;

namespace GraphQLDemo.API.Schema.Queries;


[ExtendObjectType<Query>]
public class InstructorQuery
{
    [UseDbContext(typeof(SchoolDbContext))]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<InstructorType> GetInstructors([ScopedService] SchoolDbContext context)
    {
        return context.Instructors.Select(i => new InstructorType()
        {
            Id = i.Id,
            FirstName = i.FirstName,
            LastName = i.LastName,
            Salary = i.Salary
        });
    }

    [UseDbContext(typeof(SchoolDbContext))]
    public async Task<InstructorType?> GetInstructorById(Guid id, [ScopedService] SchoolDbContext context)
    {
        var instructor = await context.Instructors.FindAsync(id);

        if (instructor == null)
        {
            return null;
        }

        return new InstructorType()
        {
            Id = instructor.Id,
            FirstName = instructor.FirstName,
            LastName = instructor.LastName,
            Salary = instructor.Salary
        };
    }
}
