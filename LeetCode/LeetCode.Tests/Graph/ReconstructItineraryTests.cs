using LeetCode.Graph.ReconstructItinerary;

namespace LeetCode.Tests.Graph;

public class ReconstructItineraryTests
{
    [Theory]
    [MemberData(nameof(GraphData))]
    public void FindItinerary_ShouldReturnExpected(IList<IList<string>> tikets, string[] expected)
        => new Solution().FindItinerary(tikets)
            .Should()
            .BeEquivalentTo(expected);

    public static IEnumerable<object[]> GraphData()
    {
        yield return new object[]
        {
            new List<IList<string>>
            {
                new List<string> { "MUC","LHR" },
                new List<string> { "JFK","MUC" },
                new List<string> { "SFO","SJC" },
                new List<string> { "LHR","SFO" }
            },
            new string[] { "JFK", "MUC", "LHR", "SFO", "SJC" }
        };

        yield return new object[]
        {
            new List<IList<string>>
            {
                new List<string> { "JFK","SFO" },
                new List<string> { "JFK","ATL" },
                new List<string> { "SFO","ATL" },
                new List<string> { "ATL", "JFK" },
                new List<string> { "ATL","SFO" }
            },
            new string[] { "JFK", "ATL", "JFK", "SFO", "ATL", "SFO" }
        };
    }
}
