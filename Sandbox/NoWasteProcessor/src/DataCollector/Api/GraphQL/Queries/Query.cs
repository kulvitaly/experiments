using Api.GraphQL.Queries.RawData;
using Application.RawContent.GetLatest;
using Common.Options;
using MediatR;

namespace Api.GraphQL.Queries;

public class Query
{
    public async Task<SourceContextType?> GetLatestSourceContent(
        [Service] ISender sender,
        CancellationToken cancellationToken)
    {
        var sourceContent = await sender.Send(new GetLatestContentQuery(), cancellationToken);
        return sourceContent
            .Map(content =>
            {
                // TODO : Introduce mapping
                return new SourceContextType()
                {
                    Id = content.Id,
                    Content = content.Content,
                    Timestamp = content.Timestamp
                };
            })
            .Reduce(default(SourceContextType)!);
    }
}
