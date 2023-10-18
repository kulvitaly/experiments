using Application.Persistence;
using Common.Options;
using Domain.RawData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RawContent.GetLatest;

public record GetLatestContentQuery() : IRequest<Option<SourceContent>>;

public class GetLatestContentQueryHandler : IRequestHandler<GetLatestContentQuery, Option<SourceContent>>
{
    private readonly ISourceDbContext _context;

    public GetLatestContentQueryHandler(ISourceDbContext context)
        => _context = context;

    public async Task<Option<SourceContent>> Handle(GetLatestContentQuery request, CancellationToken cancellationToken)
        => (await _context.SourceContents.AsNoTracking()
            .OrderByDescending(c => c.Timestamp)
            .FirstOrDefaultAsync(cancellationToken)).Optional();
}