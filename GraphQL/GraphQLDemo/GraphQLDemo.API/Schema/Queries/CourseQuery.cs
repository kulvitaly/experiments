using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;

namespace GraphQLDemo.API.Schema.Queries;

[ExtendObjectType<Query>]
public class CourseQuery
{
    private readonly CoursesRepository coursesRepo;

    public CourseQuery(CoursesRepository coursesRepo)
    {
        this.coursesRepo = coursesRepo;
    }

    [UseDbContext(typeof(SchoolDbContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    //[UseFiltering(typeof(CourseFilterType))]
    //[UseSorting(typeof(CourseSortType))]
    public async Task<IQueryable<CourseType>> GetCourses([ScopedService] SchoolDbContext context)
    {
        return context.Courses.Select(c => new CourseType()
        {
            Id = c.Id,
            Name = c.Name,
            Subject = c.Subject,
            InstructorId = c.InstructorId,
            CreatorId = c.CreatorId
        });
    }

    public async Task<CourseType?> GetCoursesByIdAsync(Guid id)
    {
        var dto = await coursesRepo.GetById(id);

        if (dto == null)
        {
            return null;
        }

        return new CourseType()
        {
            Id = dto.Id,
            Name = dto.Name,
            Subject = dto.Subject,
            InstructorId = dto.InstructorId,
            CreatorId = dto.CreatorId
        };
    }

}
