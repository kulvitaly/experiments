using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Courses
{
    public class CoursesRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public CoursesRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ICollection<CourseDto>> GetAll()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<CourseDto?> GetById(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CourseDto> Create(CourseDto courseDto)
        {
            using var context = _contextFactory.CreateDbContext();
            
            context.Courses.Add(courseDto);
            await context.SaveChangesAsync();

            return courseDto;
        }

        public async Task<CourseDto> Update(CourseDto courseDto)
        {
            using var context = _contextFactory.CreateDbContext();
            
            context.Courses.Update(courseDto);
            await context.SaveChangesAsync();

            return courseDto;
        }

        public async Task<bool> Delete(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            try
            { 
                var courseDto = new CourseDto { Id = id };
                context.Courses.Remove(courseDto);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
