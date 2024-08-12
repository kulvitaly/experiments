using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Instructors
{
    public class InstructorsRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorsRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<InstructorDto?> GetById(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Instructors.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<InstructorDto> Create(InstructorDto InstructorDto)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Instructors.Add(InstructorDto);
            await context.SaveChangesAsync();

            return InstructorDto;
        }

        public async Task<InstructorDto> Update(InstructorDto InstructorDto)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Instructors.Update(InstructorDto);
            await context.SaveChangesAsync();

            return InstructorDto;
        }

        public async Task<bool> Delete(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var InstructorDto = new InstructorDto { Id = id };
                context.Instructors.Remove(InstructorDto);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<InstructorDto>> GetInstructorsByIds(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Instructors.Where(c => keys.Contains(c.Id)).ToListAsync(cancellationToken);
        }
    }
}
