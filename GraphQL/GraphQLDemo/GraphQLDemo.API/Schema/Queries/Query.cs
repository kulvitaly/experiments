using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Schema.Queries;

public class Query
{

    [UseDbContext(typeof(SchoolDbContext))]
    public async Task<IReadOnlyCollection<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
    {
        var courses = await context.Courses.Where(c => c.Name.Contains(term))
            .Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
                CreatorId = c.CreatorId
            })
            .ToListAsync();

        var instructors = await context.Instructors.Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
            .Select(i => new InstructorType()
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Salary = i.Salary
            })
            .ToListAsync();

        return [..courses, ..instructors];
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Query the GraphQL API";

}
