using Api.GraphQL.Queries.RawData;
using Application.RawContent.Commands;
using MediatR;

namespace Api.GraphQL.Mutations;

public class Mutation
{
    public async Task<SourceContextType> AddRawContent(
        string content,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new AddRawContentMessageCommand(content), cancellationToken);

        // TODO : Introduce mapping
        return new SourceContextType()
        {
            Id = result.Id,
            Content = result.Content,
            Timestamp = result.Timestamp
        };
    }
}
