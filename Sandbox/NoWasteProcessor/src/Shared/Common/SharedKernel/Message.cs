namespace Domain.SharedKernel;

public record Message(string? Key, string Text, DateTimeOffset DateTimeUtc);


public record RawHtmlContentMessage(string Text, DateTimeOffset DateTimeUtc) : Message(null, string.Empty, DateTimeUtc);