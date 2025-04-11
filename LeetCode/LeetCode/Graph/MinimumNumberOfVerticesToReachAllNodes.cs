namespace LeetCode.Graph.MinimumNumberOfVerticesToReachAllNodes;

public class Solution
{
    public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
    {
        var roots = new bool[n];

        foreach (var edge in edges)
        {
            roots[edge[1]] = true;
        }

        return roots
            .Select((root, index) => root == false ? index : -1)
            .Where(index => index != -1)
            .ToList();
    }
}
