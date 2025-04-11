using LeetCode.Graph.MinimumNumberOfVerticesToReachAllNodes;

namespace LeetCode.Tests.Graph;

public class MinimumNumberOfVerticesToReachAllNodesTests
{
    [Theory]
    [MemberData(nameof(GraphData))]
    public void FindSmallestSetOfVertices_ShouldSucceed(int n, IList<IList<int>> edges, IList<int> expected)
        => new Solution().FindSmallestSetOfVertices(n, edges)
            .Should()
            .BeEquivalentTo(expected);

    public static IEnumerable<object[]> GraphData()
    {
        yield return new object[]
        {
            6,
            new List<IList<int>>
            {
                new List<int> { 0, 1 },
                new List<int> { 0, 2 },
                new List<int> { 2, 5 },
                new List<int> { 3, 4 },
                new List<int> { 4, 2 }
            },
            new List<int> { 0, 3 }
        };
        yield return new object[]
        {
            5,
            new List<IList<int>>
            {
                new List<int> { 0, 1 },
                new List<int> { 2, 1 },
                new List<int> { 3, 1 },
                new List<int> { 1, 4 },
                new List<int> { 2, 4 }
            },
            new List<int> { 0, 2, 3 }
        };
    }
}
