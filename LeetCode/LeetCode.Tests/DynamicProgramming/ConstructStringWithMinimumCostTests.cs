using LeetCode.DynamicProgramming.ConstructStringWithMinimumCost;

namespace LeetCode.Tests.DynamicProgramming;

public class ConstructStringWithMinimumCostTests
{
    [Theory]
    [InlineData("abcdef", new string[] { "abdef", "abc", "d", "def", "ef" }, new int[] { 100, 1, 1, 10, 5 }, 7)]
    [InlineData("aaaa", new string[] { "z", "zz", "zzz" }, new int[] { 1, 10, 100 }, -1)]
    [InlineData("n", new string[] { "n", "n", "n", "n" }, new int[] { 2, 1, 1, 1 }, 1)]
    [InlineData("fojpleczltcsunwii", new string[] { "wii", "u", "fojplecz", "i", "o", "s", "lt", "csu", "jplecz", "n", "w", "jplec", "s", "ii", "i", "sunw", "i", "sunwi", "czltc", "sunwi", "wii", "i", "w", "wii", "foj", "fojpl", "jplec", "nwi", "ojpleczltcs", "i", "c", "pleczltcs", "fojp", "w", "tcs", "i", "ojpleczltc" }, new int[] { 6, 31, 2, 31, 7, 12, 39, 19, 24, 20, 23, 17, 34, 3, 2, 21, 19, 12, 31, 34, 24, 4, 10, 28, 25, 21, 24, 31, 28, 25, 2, 20, 19, 13, 26, 37, 8 }, 57)]
    [InlineData("imgyqilhuwgrkglvvnwqicxzpehsyu", new string[] { "vnwqicxzp", "yu", "lhuwgrkglvvnwq", "g", "qilhuwgrkglvvnwqic", "mgyqilh", "w", "h", "u", "q", "m", "e", "lvvnwqicx", "k", "sy", "r", "i", "mgyqilhuwgrkglvvnwq", "h", "kglvv", "uwgrkglvvnwqicx", "qicxzpehsy", "v", "uwgrkglvvnwqic", "vvnwqicxzpe", "cxzp", "yqilhuwgrkglv", "u", "gy", "n", "l", "sy", "mgyqilhuwgrkgl", "mgyqilhuwgr", "l", "g", "qilhuwgrkglvvnwqicxz", "i", "w", "zpe", "q", "v", "i", "n" }, new int[] { 47, 64, 70, 1, 16, 14, 47, 76, 11, 3, 29, 9, 15, 74, 76, 38, 7, 28, 27, 56, 10, 60, 77, 73, 71, 16, 70, 39, 21, 36, 22, 68, 45, 51, 30, 43, 22, 64, 24, 27, 72, 75, 22, 58 }, 164)]
    public void MinimumCost_DynamicOptimized_ReturnsExpected(string target, string[] words, int[] costs, int expected)
        => new DynamicWithCachingSolution()
            .MinimumCost(target, words, costs)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("abcdef", new string[] { "abdef", "abc", "d", "def", "ef" }, new int[] { 100, 1, 1, 10, 5 }, 7)]
    [InlineData("aaaa", new string[] { "z", "zz", "zzz" }, new int[] { 1, 10, 100 }, -1)]
    [InlineData("n", new string[] { "n", "n", "n", "n" }, new int[] { 2, 1, 1, 1 }, 1)]
    [InlineData("fojpleczltcsunwii", new string[] { "wii", "u", "fojplecz", "i", "o", "s", "lt", "csu", "jplecz", "n", "w", "jplec", "s", "ii", "i", "sunw", "i", "sunwi", "czltc", "sunwi", "wii", "i", "w", "wii", "foj", "fojpl", "jplec", "nwi", "ojpleczltcs", "i", "c", "pleczltcs", "fojp", "w", "tcs", "i", "ojpleczltc" }, new int[] { 6, 31, 2, 31, 7, 12, 39, 19, 24, 20, 23, 17, 34, 3, 2, 21, 19, 12, 31, 34, 24, 4, 10, 28, 25, 21, 24, 31, 28, 25, 2, 20, 19, 13, 26, 37, 8 }, 57)]
    [InlineData("imgyqilhuwgrkglvvnwqicxzpehsyu", new string[] { "vnwqicxzp", "yu", "lhuwgrkglvvnwq", "g", "qilhuwgrkglvvnwqic", "mgyqilh", "w", "h", "u", "q", "m", "e", "lvvnwqicx", "k", "sy", "r", "i", "mgyqilhuwgrkglvvnwq", "h", "kglvv", "uwgrkglvvnwqicx", "qicxzpehsy", "v", "uwgrkglvvnwqic", "vvnwqicxzpe", "cxzp", "yqilhuwgrkglv", "u", "gy", "n", "l", "sy", "mgyqilhuwgrkgl", "mgyqilhuwgr", "l", "g", "qilhuwgrkglvvnwqicxz", "i", "w", "zpe", "q", "v", "i", "n" }, new int[] { 47, 64, 70, 1, 16, 14, 47, 76, 11, 3, 29, 9, 15, 74, 76, 38, 7, 28, 27, 56, 10, 60, 77, 73, 71, 16, 70, 39, 21, 36, 22, 68, 45, 51, 30, 43, 22, 64, 24, 27, 72, 75, 22, 58 }, 164)]
    public void MinimumCost_DynamicSolution_ReturnsExpected(string target, string[] words, int[] costs, int expected)
        => new DynamicSolution()
            .MinimumCost(target, words, costs)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("abcdef", new string[] { "abdef", "abc", "d", "def", "ef" }, new int[] { 100, 1, 1, 10, 5 }, 7)]
    [InlineData("aaaa", new string[] { "z", "zz", "zzz" }, new int[] { 1, 10, 100 }, -1)]
    [InlineData("n", new string[] { "n", "n", "n", "n" }, new int[] { 2, 1, 1, 1 }, 1)]
    [InlineData("fojpleczltcsunwii", new string[] { "wii", "u", "fojplecz", "i", "o", "s", "lt", "csu", "jplecz", "n", "w", "jplec", "s", "ii", "i", "sunw", "i", "sunwi", "czltc", "sunwi", "wii", "i", "w", "wii", "foj", "fojpl", "jplec", "nwi", "ojpleczltcs", "i", "c", "pleczltcs", "fojp", "w", "tcs", "i", "ojpleczltc" }, new int[] { 6, 31, 2, 31, 7, 12, 39, 19, 24, 20, 23, 17, 34, 3, 2, 21, 19, 12, 31, 34, 24, 4, 10, 28, 25, 21, 24, 31, 28, 25, 2, 20, 19, 13, 26, 37, 8 }, 57)]
    [InlineData("imgyqilhuwgrkglvvnwqicxzpehsyu", new string[] { "vnwqicxzp", "yu", "lhuwgrkglvvnwq", "g", "qilhuwgrkglvvnwqic", "mgyqilh", "w", "h", "u", "q", "m", "e", "lvvnwqicx", "k", "sy", "r", "i", "mgyqilhuwgrkglvvnwq", "h", "kglvv", "uwgrkglvvnwqicx", "qicxzpehsy", "v", "uwgrkglvvnwqic", "vvnwqicxzpe", "cxzp", "yqilhuwgrkglv", "u", "gy", "n", "l", "sy", "mgyqilhuwgrkgl", "mgyqilhuwgr", "l", "g", "qilhuwgrkglvvnwqicxz", "i", "w", "zpe", "q", "v", "i", "n" }, new int[] { 47, 64, 70, 1, 16, 14, 47, 76, 11, 3, 29, 9, 15, 74, 76, 38, 7, 28, 27, 56, 10, 60, 77, 73, 71, 16, 70, 39, 21, 36, 22, 68, 45, 51, 30, 43, 22, 64, 24, 27, 72, 75, 22, 58 }, 164)]
    public void MinimumCost_NonRecursive_ReturnsExpected(string target, string[] words, int[] costs, int expected)
        => new NonRecursiveSolution()
            .MinimumCost(target, words, costs)
            .Should()
            .Be(expected);
}
