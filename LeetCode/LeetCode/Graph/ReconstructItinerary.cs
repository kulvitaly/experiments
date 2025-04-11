using System.Collections.Immutable;
using System.Diagnostics;

namespace LeetCode.Graph.ReconstructItinerary;

public class Solution(Func<IList<IList<string>>, ITiketDb>? createDb = null)
{
    private readonly Func<IList<IList<string>>, ITiketDb> _createDb = createDb ?? (x => new ImmutableTiketDb(x));

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        var timer = Stopwatch.StartNew();
        var flights = _createDb(tickets);

        if (!flights.CanBuildItenerary("JFK"))
            return [];

        var result = Dfs("JFK", flights, []);

        return result;
    }

    private static IList<string> Dfs(string from, ITiketDb flights, ImmutableList<string> sequence, int deepness = 1)
    {
        if (flights.IsEmpty)
            return sequence.Add(from);

        var destinations = flights.GetDestinations(from);

        foreach (var to in destinations)
        {
            var db = flights.Remove(from, to);

            if (deepness % 3 == 0 && !db.IsIsolated(from) && !db.IsIsolated(to))
            {
                if (!AreWeaklyConnected(from, to, db))
                    continue;
            }

            var seq = Dfs(to, db, sequence.Add(from), deepness + 1);
            if (seq.Any())
                return seq;
        }

        return [];
    }

    private static bool AreWeaklyConnected(string from, string to, ITiketDb flights)
    {
        var visited = new HashSet<string>();

        var reachable = new Queue<string>();
        reachable.Enqueue(from);

        while (reachable.Any())
        {
            var current = reachable.Dequeue();
            
            if (!visited.Add(current))
                continue;

            foreach (var neighbor in flights.GetNeighbos(current))
            {
                if (neighbor == to)
                    return true;

                reachable.Enqueue(neighbor);
            }
        }

        return false;
    }
}

public interface ITiketDb
{
    bool IsEmpty { get; }

    bool IsIsolated(string name);
    IEnumerable<string> GetDestinations(string from);
    IEnumerable<string> GetNeighbos(string node);

    ITiketDb Remove(string To, string From);

    bool CanBuildItenerary(string start);
}

public class ImmutableTiketDb : ITiketDb
{
    private readonly IImmutableList<Tiket> _tikets;

    public ImmutableTiketDb(IList<IList<string>> tickets)
        => _tikets = tickets
            .Select(t => new Tiket(t[0], t[1]))
            .OrderBy(t => t.To)
            .ToImmutableList();

    private ImmutableTiketDb(IImmutableList<Tiket> tickets) => _tikets = tickets;

    public bool IsEmpty => _tikets.Count == 0;

    public IEnumerable<string> GetDestinations(string from) => _tikets.Where(t => t.From == from).Select(t => t.To);

    public ITiketDb Remove(string To, string From) => new ImmutableTiketDb(_tikets.Remove(new Tiket(To, From)));

    public bool CanBuildItenerary(string start)
    {
        var counts = new Dictionary<string, (int OutCount, int InCount)>();

        foreach (var t in _tikets)
        {
            if (counts.TryGetValue(t.From, out var pair))
            {
                counts[t.From] = pair with { OutCount = pair.OutCount + 1 };
            }
            else
            {
                counts[t.From] = new(1, 0);
            }

            if (counts.TryGetValue(t.To, out var pair2))
            {
                counts[t.To] = pair2 with { InCount = pair2.InCount + 1 };
            }
            else
            {
                counts[t.To] = new(0, 1);
            }
        }

        var startCounts = counts[start];
        var numberOfMissing = startCounts.OutCount == startCounts.InCount
            ? 0
            : 1;

        foreach (var count in counts)
        {
            if (count.Key == start)
                continue;

            if (count.Value.InCount == count.Value.OutCount)
                continue;

            if (numberOfMissing is 0)
                return false;

            if (count.Value.InCount == count.Value.OutCount + 1)
            {
                numberOfMissing--;
                continue;
            }
        }

        return true;
    }

    public bool IsIsolated(string name)
        => !_tikets.Any(t => t.From == name || t.To == name);

    public IEnumerable<string> GetNeighbos(string node)
        => _tikets.Where(t => t.To == node || t.From == node)
            .Select(t => t.To == node ? t.From : t.To);

    record Tiket(string From, string To);
}
