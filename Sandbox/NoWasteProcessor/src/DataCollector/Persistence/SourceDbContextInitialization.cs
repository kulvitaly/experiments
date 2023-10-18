using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class SourceDbContextInitializer
{
    private readonly ILogger<SourceDbContextInitializer> _logger;
    private readonly SourceDbContext _context;

    public SourceDbContextInitializer(ILogger<SourceDbContextInitializer> logger, SourceDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }
}
