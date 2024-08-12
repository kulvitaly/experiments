using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    public DbSet<CourseDto> Courses => Set<CourseDto>();
    public DbSet<InstructorDto> Instructors => Set<InstructorDto>();
    public DbSet<StudentDto> Students => Set<StudentDto>();
}
