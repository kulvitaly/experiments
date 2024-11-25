using System.Linq;

namespace LeetCode.Graph.CloneGraph;

public class Node
{
    public int val;
    public IList<Node> neighbors;

    public Node()
    {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val)
    {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }
}

public class Solution
{
    public Node CloneGraph(Node node)
    {
        if (node == null)
        {
            return null;
        }

        var createdNodes = new Dictionary<int, Node>();

        return CloneNode(node, createdNodes);
    }

    private Node CloneNode(Node original, Dictionary<int, Node> createdNodes)
    {
        if (createdNodes.TryGetValue(original.val, out var newNode))
        {
            return newNode;
        }

        newNode = new Node(original.val);
        createdNodes.Add(original.val, newNode);

        newNode.neighbors = original.neighbors
            .Select(n => CloneNode(n, createdNodes))
            .ToList();

        return newNode;
    }
}
