using Domain.Common;

namespace Domain.RawData;

public class SourceContent : IBaseEntity<int>, IAggregateRoot
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset Timestamp { get; set; }
}
