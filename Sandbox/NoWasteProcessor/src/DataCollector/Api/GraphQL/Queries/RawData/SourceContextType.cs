namespace Api.GraphQL.Queries.RawData;

public class SourceContextType
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset Timestamp { get; set; }
}
