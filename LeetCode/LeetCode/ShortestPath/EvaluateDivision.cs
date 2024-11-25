using System.Runtime.CompilerServices;

namespace LeetCode.ShortestPath.EvaluateDivision;

public class Solution
{
    public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        var graph = CreateGraph();

        return queries
            .Select(query => BreadthFirstSearch(graph, query[0], query[1]))
            .ToArray();

        Dictionary<string, Dictionary<string, double>> CreateGraph()
        {
            var graph = new Dictionary<string, Dictionary<string, double>>();

            for (var i = 0; i < equations.Count; i++)
            {
                var dividend = equations[i][0];
                var divisor = equations[i][1];
                var value = values[i];

                Add(dividend, divisor, value);
                Add(divisor, dividend, 1 / value);
            }

            return graph;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void Add(string from, string to, double value)
            {
                if (graph.TryGetValue(from, out var neighbors))
                {
                    neighbors.Add(to, value);
                }
                else
                {
                    graph.Add(from, new() { [to] = value });
                }
            }
        }
    }

    private double BreadthFirstSearch(Dictionary<string, Dictionary<string, double>> graph, string start, string end)
    {
        if (!graph.ContainsKey(start) || !graph.ContainsKey(end))
        {
            return -1;
        }

        var nodeCount = graph.Keys.Count;
        var visited = new HashSet<string>(nodeCount);
        var queue = new Queue<(string, double)>(nodeCount);

        queue.Enqueue((start, 1));

        while (queue.Any())
        {
            var (node, value) = queue.Dequeue();

            if (node == end)
            {
                return value;
            }

            visited.Add(node);

            foreach (var neighbor in graph[node])
            {
                if (!visited.Contains(neighbor.Key))
                {
                    queue.Enqueue((neighbor.Key, value * neighbor.Value));
                }
            }
        }

        return -1;
    }
}
