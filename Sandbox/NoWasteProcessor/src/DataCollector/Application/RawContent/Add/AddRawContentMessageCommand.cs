using Application.Common.MediatR;
using Application.Persistence;
using Domain.RawData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RawContent.Commands;

public record AddRawContentMessageCommand(string RawContent) : ICommand<Unit>;

public class AddRawContentMessageCommandHandler : IRequestHandler<AddRawContentMessageCommand, Unit>
{
    private readonly ISourceDbContext _context;

    public AddRawContentMessageCommandHandler(ISourceDbContext context)
        => _context = context;

    public async Task<Unit> Handle(AddRawContentMessageCommand request, CancellationToken cancellationToken)
    {
        if (await IsDuplicate(request.RawContent, cancellationToken))
        {
            return Unit.Value;
        }

        await _context.SourceContents.AddAsync(new SourceContent
        {
            Content = request.RawContent,
            Timestamp = DateTimeOffset.UtcNow
        }, cancellationToken);

        return Unit.Value;
    }

    private async Task<bool> IsDuplicate(string content, CancellationToken cancellationToken)
    {
        var lastMessage = await _context.SourceContents.AsNoTracking()
            .OrderByDescending(c => c.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);

        return lastMessage != null && lastMessage.Content.Equals(content, StringComparison.OrdinalIgnoreCase);
    }
}
