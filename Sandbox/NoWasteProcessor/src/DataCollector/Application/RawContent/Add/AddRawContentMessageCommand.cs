using Application.Common.MediatR;
using Application.Persistence;
using Domain.RawData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RawContent.Commands;

public record AddRawContentMessageCommand(string RawContent) : ICommand<SourceContent>;

public class AddRawContentMessageCommandHandler : IRequestHandler<AddRawContentMessageCommand, SourceContent>
{
    private readonly ISourceDbContext _context;

    public AddRawContentMessageCommandHandler(ISourceDbContext context)
        => _context = context;

    public async Task<SourceContent> Handle(AddRawContentMessageCommand request, CancellationToken cancellationToken)
    {
        var lastMessage = await _context.SourceContents.AsNoTracking()
            .OrderByDescending(c => c.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);

        if (IsDuplicate(lastMessage, request.RawContent, cancellationToken))
        {
            return lastMessage!;
        }

        var sourceContent = new SourceContent
        {
            Content = request.RawContent,
            Timestamp = DateTimeOffset.UtcNow
        };

        await _context.SourceContents.AddAsync(sourceContent, cancellationToken);

        return sourceContent;
    }

    private bool IsDuplicate(SourceContent? source, string content, CancellationToken cancellationToken)
        => source != null && source.Content.Equals(content, StringComparison.OrdinalIgnoreCase);
}
