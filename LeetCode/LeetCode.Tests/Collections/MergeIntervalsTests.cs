using LeetCode.Collections.MergeIntervals;

namespace LeetCode.Tests.Collections;

public class MergeIntervalsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void MergeIntervals_ShouldSucceed(int[][] intervals, int[][] expecedIntervals)
    {
        // Act 
        var merged = new Solution().Merge(intervals);

        // Assert
        merged.Should().BeEquivalentTo(expecedIntervals);
    }

    public static IEnumerable<object[]> TestData =
        [
            [ new[] { new int[] { 1, 3 }, [2, 6], [8, 10], [15, 18] }, new[] { new int[] { 1, 6 }, [8, 10], [15, 18] } ],
            [ new[] { new int[] { 1, 4 }, [4, 5] }, new[] { new int[] { 1, 5 } } ],
            [ new[] { new int[] { 1, 4 }, [0, 0] }, new[] { new int[] { 0, 0 }, [1, 4] } ],
        ];
}
