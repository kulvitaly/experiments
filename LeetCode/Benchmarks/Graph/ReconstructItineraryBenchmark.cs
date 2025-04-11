using BenchmarkDotNet.Attributes;
using LeetCode.Graph.ReconstructItinerary;

namespace Benchmarks.Graph;

public class ReconstructItineraryBenchMark
{
    [Params(1)]
    public int Count { get; set; } = 1;

    public List<IList<string>> Tikets = new List<IList<string>>()
        { new List<string> {"JFK", "SFO"}, new List<string> {"JFK", "ATL"}, new List<string> {"SFO", "JFK"}, new List<string> {"ATL", "AAA"}, new List<string> {"AAA", "ATL"}, new List<string> {"ATL", "BBB"}, new List<string> {"BBB", "ATL"}, new List<string> {"ATL", "CCC"}, new List<string> {"CCC", "ATL"}, new List<string> {"ATL", "DDD"}, new List<string> {"DDD", "ATL"}, new List<string> {"ATL", "EEE"}, new List<string> {"EEE", "ATL"}, new List<string> {"ATL", "FFF"}, new List<string> {"FFF", "ATL"}, new List<string> {"ATL", "GGG"}, new List<string> {"GGG", "ATL"}, new List<string> {"ATL", "HHH"}, new List<string> {"HHH", "ATL"}, new List<string> {"ATL", "III"}, new List<string> {"III", "ATL"}, new List<string> {"ATL", "JJJ"}, new List<string> {"JJJ", "ATL"}, new List<string> {"ATL", "KKK"}, new List<string> {"KKK", "ATL"}, new List<string> {"ATL", "LLL"}, new List<string> {"LLL", "ATL"}, new List<string> {"ATL", "MMM"}, new List<string> {"MMM", "ATL"}, new List<string> {"ATL", "NNN"}, new List<string> {"NNN", "ATL"} };

    [Benchmark]
    public void ImmutableCollectionSolution() => RunBenchmark(new Solution(x => new ImmutableTiketDb(x)));

    private void RunBenchmark(Solution solution)
    {
        try
        {
            for (int i = 0; i < Count; ++i)
            {
                var itinerary = solution.FindItinerary(Tikets);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed: exception {ex.GetType()}: {ex.Message}");
        }
    }
}
